using ModernSchool.ViewsModels;

namespace ModernSchool;

public static class Utilities
{
    public static Dictionary<string, string[]> IsValid(StudentItemDTO td)
    {
        Dictionary<string, string[]> errors = new();

        if (string.IsNullOrEmpty(td.Nom))
        {
            errors.TryAdd("todo.name.errors", new[] { "Name is empty" });
        }

        if (td.Nom.Length < 3)
        {
            errors.TryAdd("todo.name.errors", new[] { "Name length < 3" });
        }

        return errors;
    }
}