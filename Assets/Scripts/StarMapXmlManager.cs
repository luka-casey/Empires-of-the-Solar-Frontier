using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class StarMapXmlManager
{
    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "starmap_dummy_test.xml");

    public static void Save(StarMapSaveData starMapSaveData)
    {
        var serializer = new XmlSerializer(typeof(StarMapSaveData));

        using var stream = new FileStream(FilePath, FileMode.Create);
        serializer.Serialize(stream, starMapSaveData);

        //Debug.Log($"Save written to:\n{FilePath}");
    }

    public static StarMapSaveData Load()
    {
        if (!File.Exists(FilePath))
        {
            //Debug.LogError("Save file not found!");
            return null;
        }

        var serializer = new XmlSerializer(typeof(StarMapSaveData));

        using var stream = new FileStream(FilePath, FileMode.Open);
        StarMapSaveData data = (StarMapSaveData)serializer.Deserialize(stream);

        return data;
    }
}
