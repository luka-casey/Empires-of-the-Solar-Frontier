using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class XmlManager
{
    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "dummy_test.xml");

    public static void Save(Colony colonyData)
    {
        var serializer = new XmlSerializer(typeof(Colony));

        using var stream = new FileStream(FilePath, FileMode.Create);
        serializer.Serialize(stream, colonyData);

        Debug.Log($"Save written to:\n{FilePath}");
    }

    public static void CreateNewColony()
    {
        // #region NewColonyObject

        // var productionData = new List<Production>
        // {
        //     new Production(7, "Solder", "5", ProductionTypeEnum.Unit),
        //     new Production(7, "Ship", "9", ProductionTypeEnum.Unit)
        // };

        // var newColonyObject = new Colony
        // {
        //     //colonyId = 1,
        //     //colonyName = "Victoria",
        //     population = 3,
        //     income = 3,
        //     expenses = 3,
        //     production = 3,
        //     science = 3,
        //     history = "3",
        //     buildings = buildingsData,
        //     productions = productionData
        // };

        // #endregion
            
        // var serializer = new XmlSerializer(typeof(Colony));

        // using var stream = new FileStream(FilePath, FileMode.Create);
        // serializer.Serialize(stream, newColonyObject);

        // Debug.Log($"Save written to:\n{FilePath}");
    }

    public static Colony Load()
    {
        if (!File.Exists(FilePath))
        {
            Debug.LogError("Save file not found!");
            return null;
        }

        var serializer = new XmlSerializer(typeof(Colony));

        using var stream = new FileStream(FilePath, FileMode.Open);
        Colony data = (Colony)serializer.Deserialize(stream);

        return data;
    }
}
