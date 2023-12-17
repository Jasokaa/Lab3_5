using System.Xml.Serialization;

namespace DAL;
public class XMLProvider<T>
{
    private const string file = "file.xml";
    public void Write(List<T>? list)
    {
        File.WriteAllText(file, string.Empty);
        using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
            formatter.Serialize(fileStream, list);
        }
    }
    public List<T>? Read()
    {
        List<T>? deserializedObjects;
        using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
            deserializedObjects = (List<T>)formatter.Deserialize(fileStream)!;
        }
        return deserializedObjects;
    }
    public void DeleteAllFromFile()
    {
        File.WriteAllText(file, string.Empty);
    }
}