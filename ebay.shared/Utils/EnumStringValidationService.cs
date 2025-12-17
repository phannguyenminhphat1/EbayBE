public static class EnumValidationStringService
{
    public static bool IsValidName<TEnum>(string value) where TEnum : Enum
    {
        return Enum.TryParse(typeof(TEnum), value, ignoreCase: true, out _);
    }

    public static string GetValidEnumNames<TEnum>() where TEnum : Enum
    {
        return string.Join(", ", Enum.GetNames(typeof(TEnum)));
    }
}
