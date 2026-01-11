using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class ShipXmlManager
{
    public static Ship LoadShipData(string shipName)
    {
        var path = Path.Combine(Application.persistentDataPath, $"{shipName}.xml");

        if (!File.Exists(path))
        {
            Debug.LogError($"{path} not found!");
            return null;
        }

        var serializer = new XmlSerializer(typeof(Ship));

        using var stream = new FileStream(path, FileMode.Open);
        Ship data = (Ship)serializer.Deserialize(stream);

        return data;
    }

    public static void SaveShipData(Ship shipData)
    {
        var path = Path.Combine(Application.persistentDataPath, $"{shipData.shipName}.xml");

        if (!File.Exists(path))
        {
            Debug.LogError($"{path} not found!");
        }

        var serializer = new XmlSerializer(typeof(Ship));

        using var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, shipData);
    }
}
