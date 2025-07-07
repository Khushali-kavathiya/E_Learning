namespace  E_Learning.Domain.Enums;

/// <summary>
/// Represents the different roles a user can have in the system.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Represents a user with student privileges.
    /// </summary>
    Student,

    /// <summary>       
    /// Represents a user with instructor privileges.
    /// </summary>
    Instructor,

    /// <summary>
    /// Represents a user with admin privileges.
    /// </summary>
    Admin
}