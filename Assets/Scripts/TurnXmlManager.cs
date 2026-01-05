using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class TurnXmlManager
{
    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "turn_manager_test.xml");

    public static void Save(Turn turn)
    {
        var serializer = new XmlSerializer(typeof(Turn));

        using var stream = new FileStream(FilePath, FileMode.Create);
        serializer.Serialize(stream, turn);

        //Debug.Log($"Save written to:\n{FilePath}");
    }

    public static Turn Load()
    {
        if (!File.Exists(FilePath))
        {
            //Debug.LogError("Save file not found!");
            return null;
        }

        var serializer = new XmlSerializer(typeof(Turn));

        using var stream = new FileStream(FilePath, FileMode.Open);
        Turn data = (Turn)serializer.Deserialize(stream);

        return data;
    }
}
