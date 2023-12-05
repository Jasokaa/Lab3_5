using System.Text.RegularExpressions;
using DAL;
namespace BLL;
public class EntityService<T>
{
    public List<T> AddEntity(List<T>? list, T input)
    {
        list.Add(input);
        return list;
    }
    public List<T>? RemoveEntityByIndex(List<T>? list, int index)
    {
        if (index >= 0 && index < list.Count)
        {
            list.RemoveAt(index);
            return list;
        }
        else
        {
            throw new Exception("Index out of range");
        }
    }
    
    public bool InputName(string? info)//firstName or lastName
    {
        Regex regex = new Regex(@"^[A-Z]{1}[a-z]+$");
        return regex.IsMatch(info);
    }
    public bool InputCourse(string? info)
    {
        Regex regex = new Regex(@"^[1-6]$");
        return regex.IsMatch(info);
    }
    public bool InputStudentCard(string? info)
    {
        Regex regex = new Regex(@"^KB\d{8}$");
        return regex.IsMatch(info);
    }
    public bool InputBirthDate(string? info)
    {
        Regex regexDate = new Regex(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])$");
        Regex regexMonth = new Regex(@"^(0[1-9]|1[0-2])$");
        Regex regexYear = new Regex(@"^\d{4}$");
        try
        {
            string[] data = info.Split(".");
            if (regexDate.IsMatch(data[0]) && regexMonth.IsMatch(data[1]) && regexYear.IsMatch(data[2]))
            {
                int intDate = Int32.Parse(data[0]);
                int intMonth = Int32.Parse(data[1]);
                if (((intDate == 31 && (intMonth == 01 || intMonth == 03 || intMonth == 05 || intMonth == 07 || intMonth == 08  || intMonth == 10  || intMonth == 12))
                      || (intDate == 30 && intMonth != 02) || intDate < 30))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public List<Student> SearchStudent(List<Entity>? list)//Calculate the number of 4th-year students born in the spring.
    {
        List<Student> studentsList = list// Use LINQ
            .OfType<Student>()
            .ToList();
        List<Student> filteredStudentsList = studentsList
            .Where(student => student.Course == 4 && SearchSpring(student))
            .ToList();
        return filteredStudentsList;
    }
    public bool SearchSpring(Student student)
    {
        string? info = student.BirthDate;
        string[] data = info.Split(".");
        if (data[1] == "03" || data[1] == "04" || data[1] == "05")
        {
            return true;
        }

        return false;
    }
    
}