using DAL;

namespace BLL;

public class CreateService
{
    public void createStudent(string? fName, string? lName, int course, string? sCard, string? bDate)
    {
        Student student = new Student(fName, lName, course, sCard, bDate);
        EntityService<Entity> a = new EntityService<Entity>();
        DBService<Entity> b = new DBService<Entity>();
        List<Entity>? list = new List<Entity>();
        if (b.ReadDB() != null)
        {
            list = b.ReadDB();
        }
        list = a.AddEntity(list, student);
        b.WriteDB(list);
    }
    public void createBaker(string? fName, string? lName)
    {
        Baker baker = new Baker(fName, lName);
        EntityService<Entity> a = new EntityService<Entity>();
        DBService<Entity> b = new DBService<Entity>();
        List<Entity>? list = new List<Entity>();
        if (b.ReadDB() != null)
        {
            list = b.ReadDB();
        }
        list = a.AddEntity(list, baker);
        b.WriteDB(list);
    }
    public void createEntrepreneur(string? fName, string? lName)
    {
        Entrepreneur entrepreneur = new Entrepreneur(fName, lName);
        EntityService<Entity> a = new EntityService<Entity>();
        DBService<Entity> b = new DBService<Entity>();
        List<Entity>? list = new List<Entity>();
        if (b.ReadDB() != null)
        {
            list = b.ReadDB();
        }
        list = a.AddEntity(list, entrepreneur);
        b.WriteDB(list);
    }
}