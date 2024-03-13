using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadPlayerData
{
    readonly static string SAVE_FILE_PATH = Application.persistentDataPath + "/player.dt";

    public static PlayerData Load()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Open);

            PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            return playerData;
        }
        else
        {
            Debug.LogError("Save file not found in " + SAVE_FILE_PATH);
            return null;
        }
    }
}
