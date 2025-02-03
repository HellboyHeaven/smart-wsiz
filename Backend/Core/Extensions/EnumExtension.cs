namespace Core.Extensions;
public static class EnumExtension
{
    public static T ParseEnum<T>(this string value) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}
