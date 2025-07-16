namespace E_Learning.Extensions.Attributes;


/// <summary>
/// Prevents a property from being updated once created (e.g., Email).
/// </summary>

[AttributeUsage(AttributeTargets.Property)]
public class CreateOnlyAttribute : Attribute
{
}