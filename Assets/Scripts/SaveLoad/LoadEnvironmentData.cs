using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LoadEnvironmentData
{
    readonly static string SAVE_FILE_PATH = Application.persistentDataPath + "/environment.dt";

    public static EnvironmentData Load()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Open);

            EnvironmentData environmentData = (EnvironmentData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            return environmentData;
        }
        else
        {
            Debug.LogError("Save file not found in " + SAVE_FILE_PATH);
            return null;
        }
    }
}
