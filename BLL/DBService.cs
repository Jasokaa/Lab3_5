namespace BLL;
using DAL;
public class DBService<T>
{
    public void WriteDB(List<T>? list)
    {
        try
        {
            JSONProvider<T> b = new JSONProvider<T>();
            b.Write(list);
        }
        catch (Exception)
        {
            // ignored
        }
    }
    public List<T>? ReadDB()
    {
        List<T>? list = null;
        try
        {
            JSONProvider<T> a = new JSONProvider<T>();
            list = a.Read();
        }
        catch (Exception)
        {
            // ignored
        }
        return list;
    }
    public void DeleteAllFromFile()
    {
        try
        {
            JSONProvider<T> a = new JSONProvider<T>();
            a.DeleteAllFromFile();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}