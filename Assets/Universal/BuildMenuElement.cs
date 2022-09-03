using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class BuildMenuElement
{
    public string Title;
    public BuildPanel.Category Category;
    public BuildingType BuildingType;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(BuildMenuElement));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static BuildMenuElement Load(string path)
    {
        var serializer = new XmlSerializer(typeof(BuildMenuElement));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as BuildMenuElement;
        }
    }

    public static BuildMenuElement LoadText(string text)
    {
        var serializer = new XmlSerializer(typeof(BuildMenuElement));
        using (var stream = new System.IO.StringReader(text))
        {
            return serializer.Deserialize(stream) as BuildMenuElement;
        }
    }
}
