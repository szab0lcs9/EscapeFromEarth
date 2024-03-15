using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveEnvironmentData
{
    readonly static string SAVE_FILE_PATH = Application.persistentDataPath + "/environment.dt";

    public static void Save(GameObject[] celestialBodies, GameObject spaceStation)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Create);

        EnvironmentData environmentData = new EnvironmentData(celestialBodies, spaceStation);

        binaryFormatter.Serialize(fileStream, environmentData);
        fileStream.Close();
    }
}
