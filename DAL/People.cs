using System.Text.Json.Serialization;

namespace DAL;
public interface IEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
[JsonDerivedType(typeof(Student), typeDiscriminator: "Student")]
[JsonDerivedType(typeof(Baker), typeDiscriminator: "Baker")]
[JsonDerivedType(typeof(Entrepreneur), typeDiscriminator: "Entrepreneur")]
public abstract class Entity:IEntity
{
    protected string? firstName;
    protected string? lastName;
    public string? FirstName { get => firstName; set => firstName = value; }
    public string? LastName { get => lastName; set => lastName = value; }
    [JsonConstructor]
    protected Entity(string? FirstNameInput, string? LastNameInput)
    {
        firstName = FirstNameInput;
        lastName = LastNameInput;
    }

    protected Entity() {}
}
public class Student : Entity, IStudy
{
    private int course;
    private string? studentCard;
    private string? birthDate;


    public Student() { }
    
    public int Course
    {
        get => course;
        set => course = value;
    }
    public string? StudentCard
    {
        get => studentCard;
        set => studentCard = value;
    }
    public string? BirthDate
    {
        get => birthDate;
        set => birthDate = value;
    }
    [JsonConstructor]
    public Student(string? FirstName, string? LastName, int Course, string? StudentCard, string? BirthDate) : 
        base(FirstName, LastName)
    {
        course = Course;
        studentCard = StudentCard;
        birthDate = BirthDate;
    }
    public override string ToString()
    {
        return "EntityType Student " + 
               "{FirstName: '" + firstName + "', " +
               "LastName: '" + lastName + "', " +
               "Course: '" + course + "', " +
               "StudentCard: '" + studentCard + "', " +
               "BirthDate: '" + birthDate + "'};";
    }

    public string Study()
    {
        Random rnd = new Random();
        int mark = rnd.Next(60, 100);
        return firstName + " " + lastName + " studied and got " + mark + " on test";
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        Student otherStudent = (Student)obj;
        return firstName == otherStudent.FirstName &&
               lastName == otherStudent.LastName &&
               course == otherStudent.Course &&
               lastName == otherStudent.LastName &&
               studentCard == otherStudent.StudentCard &&
               birthDate == otherStudent.BirthDate;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
public class Baker : Entity, IBake
{
    public Baker(){}
    [JsonConstructor]
    public Baker(string? FirstName, string? LastName):base(FirstName, LastName){}
    public override string ToString()
    {
        return "EntityType Baker " + 
               "{FirstName: '" + firstName + "', " +
               "LastName: '" + lastName + "'};";
    }
    public string Bake()
    {
        return firstName + " "+ lastName + " baked bread";
    }
}
public class Entrepreneur : Entity, IWork
{
    public Entrepreneur(){}
    [JsonConstructor]
    public Entrepreneur(string? FirstName, string? LastName):base(FirstName, LastName){}
    public override string ToString()
    {
        return "EntityType Entrepreneur " + 
               "{FirstName: '" + firstName + "', " +
               "LastName: '" + lastName + "'};";
    }
    public string Work()
    {
        Random rnd = new Random();
        int money = rnd.Next(1, 100);
        return firstName + " " + lastName + " earned $" + money + "000";
    }
}
