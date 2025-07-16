using System.Reflection.Emit;
using Microsoft.AspNetCore.JsonPatch;
using System.Reflection;
using E_Learning.Extensions.Attributes;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace E_Learning.Extensions.Helpers;

/// <summary>
/// Provides filtering logic to remove operations that try to update [CreateOnly] properties.
/// </summary>
public static class PatchFilter 
{
    /// <summary>
    /// Filters out patch operations that target [CreateOnly] properties from the contract.
    /// </summary>
    /// <typeparam name="T">Type of the contract.</typeparam>
    /// <param name="patchDoc">Incoming patch document.</param>

    public static void RemoveCreateOnlyFields<T>(JsonPatchDocument<T> patchDoc) where T : class
    {
        if (patchDoc == null || patchDoc.Operations == null)
            return;

        var createOnlyProp = typeof(T)
            .GetProperties()
            .Where(p => p.GetCustomAttribute<CreateOnlyAttribute>() != null)
            .Select(p => "/" + p.Name[..1].ToLower() + p.Name[1..])
            .ToHashSet();

        patchDoc.Operations.RemoveAll(op =>
        createOnlyProp.Contains(op.path) &&
        (op.OperationType == OperationType.Replace || op.OperationType == OperationType.Add));  
    }
} 