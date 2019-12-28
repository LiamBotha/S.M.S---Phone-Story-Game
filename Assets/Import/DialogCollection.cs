using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("DialogCollection")]
public class DialogCollection
{

    [XmlArray("Records")]
    [XmlArrayItem("record")]
    public List<Dialog> dialogs = new List<Dialog>();

    public static DialogCollection DialogLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(DialogCollection));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as DialogCollection;
        }
    }
}

public class Dialog
{
    public int ID;
    public string Description;
    public int Character;
    public string Trigger;
}
