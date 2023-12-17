namespace BLL;
using DAL;
public class DBService<T>
{
    public void WriteDB(List<T>? list)
    {
        try
        {
            XMLProvider<T> b = new XMLProvider<T>();
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
            XMLProvider<T> b = new XMLProvider<T>();
            list = b.Read();
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
            XMLProvider<T> b = new XMLProvider<T>();
            b.DeleteAllFromFile();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}