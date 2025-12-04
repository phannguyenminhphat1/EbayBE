public class EnumValidationService
{
    public static bool IsValid<TEnum>(int value) where TEnum : Enum
    {
        return Enum.IsDefined(typeof(TEnum), value);
    }

    public static string GetValidEnumValues<TEnum>() where TEnum : Enum
    {
        return string.Join(", ", Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(s => $"{Convert.ToInt32(s)} - {s}"));
    }
}
