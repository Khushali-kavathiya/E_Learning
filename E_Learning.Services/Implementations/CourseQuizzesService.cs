using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Repositories.Interfaces;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace E_Learning.Services.Implementations
{
    /// <summary>
    /// Service for generating quizzes using AI and saving them to the database.
    /// </summary>
    public class CourseQuizzesService(
        ICourseQuizzesRepository _courseQuizzesRepository,
        IHttpClientFactory _httpClientFactory,
        IConfiguration _configuration,
        IMapper _mapper) : ICourseQuizzesService
    {
        /// <inheritdoc />
        public async Task GenerateQuizAsync(CourseQuizModel model)
        {
            var client = _httpClientFactory.CreateClient();

            // Set OpenRouter API headers
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["OpenRouter:ApiKey"]);

            // Prompt to generate quiz questions
            var prompt = $"Generate 5 {model.Difficulty} level multiple choice questions from this content:\n\n{model.CourseContentText}\n\nFormat:\nQ1. Question?\na) Option A\nb) Option B\nc) Option C\nd) Option D\nAnswer: b";

            var requestBody = new
            {
                model = "google/gemma-3-12b-it",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenRouter API failed: {response.StatusCode} - {responseContent}");
            }

            // Extract AI-generated quiz content
            using var doc = JsonDocument.Parse(responseContent);
            string generatedText = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? throw new Exception("Empty AI response.");

            // Parse questions
            var quizQuestions = ParseQuestionsFromText(generatedText, model.Difficulty);

            if (!quizQuestions.Any())
            {
                throw new Exception("No valid quiz questions could be parsed from the AI response.");
            }

            var quiz = new CourseQuiz
            {
                Id = Guid.NewGuid(),
                CourseId = model.CourseId,
                Questions = quizQuestions
            };

            await _courseQuizzesRepository.SaveAsync(quiz);
        }

        // Helper method to parse AI-generated quiz questions
        private static List<QuizQuestion> ParseQuestionsFromText(string aiText, string difficulty)
        {
            var questions = new List<QuizQuestion>();
            var blocks = aiText.Split("Q").Skip(1); // Skip before Q1

            foreach (var block in blocks)
            {
                try
                {
                    var lines = block.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var questionLine = lines.FirstOrDefault(l => l.Contains("."))?.Trim();

                    var options = lines
                        .Where(l => l.StartsWith("a)") || l.StartsWith("b)") || l.StartsWith("c)") || l.StartsWith("d)"))
                        .Select(l => l.Trim())
                        .ToList();

                    var answerLine = lines.FirstOrDefault(l => l.StartsWith("Answer:", StringComparison.OrdinalIgnoreCase));

                    if (questionLine == null || options.Count != 4 || answerLine == null)
                        continue;

                    var questionText = questionLine[(questionLine.IndexOf('.') + 1)..].Trim();
                    var answerLetter = answerLine.Split(':')[1].Trim().ToLower();

                    var quizOptions = new List<QuizOption>();
                    foreach (var opt in options)
                    {
                        var letter = opt.Substring(0, 1).ToLower();
                        var text = opt[2..].Trim();

                        quizOptions.Add(new QuizOption
                        {
                            Id = Guid.NewGuid(),
                            Text = text,
                            IsCorrect = letter == answerLetter
                        });
                    }

                    var correctAnswerText = quizOptions.FirstOrDefault(o => o.IsCorrect)?.Text ?? "";

                    questions.Add(new QuizQuestion
                    {
                        Id = Guid.NewGuid(),
                        QuestionText = questionText,
                        Difficulty = difficulty,
                        CorrectAnswer = correctAnswerText,
                        Options = quizOptions
                    });
                }
                catch
                {
                    // Skip malformed blocks
                    continue;
                }
            }

            return questions;
        }

        /// <inheritdoc />
        public async Task<CourseQuizModel> GetQuizByCourseIdAsync(Guid courseId)
        {
            var quiz = await _courseQuizzesRepository.GetByCourseIdAsync(courseId);
            if (quiz == null)
                throw new KeyNotFoundException($"Quiz not found for courseId: {courseId}");

            return _mapper.Map<CourseQuizModel>(quiz);
        }
    }
}
