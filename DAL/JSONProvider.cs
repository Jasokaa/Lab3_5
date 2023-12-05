using System.Text.Json;
namespace DAL;
public class JSONProvider<T>
{
    private const string file = "fileJSON.json";
    public JSONProvider() {}
    public void Write(List<T>? list)
    {
        File.WriteAllText(file, string.Empty);
        using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fileStream, list);
        }
    }
    public List<T>? Read()
    {
        using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
        {
            return (List<T>)JsonSerializer.Deserialize(fileStream, typeof(List<T>));
        }
    }
    public void DeleteAllFromFile()
    {
        File.WriteAllText(file, string.Empty);
    }
}