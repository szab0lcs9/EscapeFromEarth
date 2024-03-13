using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LoadEnemyData
{
    readonly static string SAVE_FILE_PATH = Application.persistentDataPath + "/enemy.dt";

    public static EnemyData Load()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Open);

            EnemyData enemyData = (EnemyData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            return enemyData;
        }
        else
        {
            Debug.LogError("Save file not found in " + SAVE_FILE_PATH);
            return null;
        }
    }
}
