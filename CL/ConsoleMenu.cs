using BLL;
using DAL;
namespace CL;
public abstract class ConsoleMenu
{
    private static EntityService<Entity> entityService = new EntityService<Entity>();
    private static CreateService createService = new CreateService();
    private static DBService<Entity> dbService = new DBService<Entity>();
    public static string commands()
    {
        Console.Clear();
        string commandsSTR = "To choose command write:\n" +
                          "1 - add student\n" +
                          "2 - add baker\n" +
                          "3 - add entrepreneur\n" +
                          "4 - show database\n" +
                          "5 - delete someone\n" + 
                          "6 - calculate the number and show 4th-year students born in the spring\n" +
                          "7 - delete all from database\n" +
                          "8 - add some entities\n" +
                          "EXIT - stop program\n";
        Console.Write(commandsSTR);
        return $"{Console.ReadLine()}";
    }
    
    public static int userInput(string input)
    {
        try
        {
            switch (input)
            {
                case "1":
                {
                    addStudent();
                    return 0;
                }
                case "2":
                {
                    addBaker();
                    return 0;
                }
                case "3":
                {
                    addEntrepreneur();
                    return 0;
                }
                case "4":
                {
                    showDatabase();
                    return 0;
                }
                case "5":
                {
                    deleteSomeone();
                    return 0;
                }
                case "6":
                {
                    calculate();
                    return 0;
                }
                case "7":
                {
                    deleteDB();
                    return 0;
                }
                case "8":
                {
                    addEntities();
                    return 0;
                }
                case "EXIT":
                {
                    return 1;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return 0;
    }

    private static void addStudent()
    {
        Console.Clear();
        
        Console.WriteLine("Enter first name:");
        string? data = Console.ReadLine();
        while (entityService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? firstName = data;
        
        Console.WriteLine("Enter last name:");
        data = Console.ReadLine();
        while (entityService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? lastName = data;
        
        Console.WriteLine("Enter course:");
        data = Console.ReadLine();
        while (entityService.InputCourse(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        int course = Int32.Parse(data);
        
        Console.WriteLine("Enter student card in format (KB12345678):");
        data = Console.ReadLine();
        while (entityService.InputStudentCard(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? sCard = data;
        
        Console.WriteLine("Enter birth date in format (01.01.2001):");
        data = Console.ReadLine();
        while (entityService.InputBirthDate(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? bDate = data;
        createService.createStudent(firstName, lastName, course, sCard, bDate);
    }
    
    private static void addBaker()
    {
        Console.Clear();
        
        Console.WriteLine("Enter first name:");
        string? data = Console.ReadLine();
        while (entityService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? firstName = data;
        
        Console.WriteLine("Enter last name:");
        data = Console.ReadLine();
        while (entityService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? lastName = data;
        createService.createBaker(firstName, lastName);
    }

    private static void addEntrepreneur()
    {
        Console.Clear();
        
        Console.WriteLine("Enter first name:");
        string? data = Console.ReadLine();
        while (entityService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? firstName = data;
        
        Console.WriteLine("Enter last name:");
        data = Console.ReadLine();
        while (entityService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? lastName = data;
        createService.createEntrepreneur(firstName, lastName);
    }

    private static void showDatabase()
    {
        Console.Clear();
        List<Entity>? list = new List<Entity>();
        if (dbService.ReadDB() != null)
        {
            list = dbService.ReadDB();
            foreach (Entity line in list)
            {
                if (line != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
           Console.WriteLine("DB is empty."); 
        }
        
        Console.ReadLine();
    }

    private static void deleteSomeone()
    {
        Console.Clear();
        List<Entity>? list = dbService.ReadDB();
        int index = 0;
        foreach (Entity line in list)
        {
            if (line != null)
            {
                Console.WriteLine(index + ": " + line);
                index++;
            }
        }

        while (true)
        {
            try
            {
                string? input = Console.ReadLine();
                if(!(Int32.Parse(input) >= 0 && Int32.Parse(input)< list.Count))
                {
                    Console.WriteLine("ERROR! Try again to write:");
                    input = Console.ReadLine();
                }
                List<Entity>? newlist = entityService.RemoveEntityByIndex(list,Int32.Parse(input));
                if (newlist != null)
                {
                    dbService.WriteDB(newlist);
                }
                else
                {
                    dbService.DeleteAllFromFile();
                }
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
    
    private static void calculate()
    {
        Console.Clear();
        List<Entity>? list = new List<Entity>();
        list = dbService.ReadDB();
        List<Student> studentList = entityService.SearchStudent(list);
        Console.WriteLine("There are " + studentList.Count + " student of 4-course born in the spring:");
        foreach (var student in studentList)
        {
            Console.WriteLine(student);
        }
        Console.ReadLine();
    }

    private static void deleteDB()
    {
        Console.Clear();
        dbService.DeleteAllFromFile();
        Console.WriteLine("Now DB file is empty");
        Console.ReadLine();
    }
    
    private static void addEntities()
    {
        createService.createStudent("Tom", "Rox", 4, "KB12312312", "03.03.2002");
        createService.createBaker("Nana", "Fai");
        createService.createStudent("Bob", "Wein", 4, "KB89898989", "20.11.2003");
        createService.createStudent("Kate", "Lu", 3, "KB09809809", "30.05.2001");
        createService.createStudent("Sem", "Tofi", 4, "KB89898989", "15.04.2003");
        createService.createEntrepreneur("Jon", "Vels");
        createService.createStudent("Dan", "Koi", 4, "KB56565656", "05.05.2000");
    }
}