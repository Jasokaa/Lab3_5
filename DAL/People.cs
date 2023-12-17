using System.Xml.Serialization;

namespace DAL;
public interface IEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
[Serializable]
[XmlInclude(typeof(Student))]
[XmlInclude(typeof(Baker))]
[XmlInclude(typeof(Entrepreneur))]
public abstract class Entity:IEntity
{
    protected string? firstName;
    protected string? lastName;
    public string? FirstName { get => firstName; set => firstName = value; }
    public string? LastName { get => lastName; set => lastName = value; }
    protected Entity(string? FirstNameInput, string? LastNameInput)
    {
        firstName = FirstNameInput;
        lastName = LastNameInput;
    }

    protected Entity() {}
}
[Serializable]
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
[Serializable]
public class Baker : Entity, IBake
{
    public Baker(){}
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
[Serializable]
public class Entrepreneur : Entity, IWork
{
    public Entrepreneur(){}
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
