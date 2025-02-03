namespace Core.Models.Users;



public class Teacher : User
{
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    public List<Module> Modules { get; set; } = new List<Module>();

    public override string ToString()
    {
        return $"dr {Firstname} {Lastname}";
    }
}