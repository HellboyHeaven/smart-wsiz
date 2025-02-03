namespace Core;

[AttributeUsage(AttributeTargets.Enum)]
public class EnumAttribute : Attribute
{
    public string DatabaseName { get; }

    public EnumAttribute(string databaseName)
    {
        DatabaseName = databaseName;
    }
}