using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveEnemyData
{
    readonly static string SAVE_FILE_PATH = Application.persistentDataPath + "/enemy.dt";

    public static void Save(Alien alien)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Create);

        EnemyData enemyData = new EnemyData(alien);

        binaryFormatter.Serialize(fileStream, enemyData);
        fileStream.Close();
    }
}
