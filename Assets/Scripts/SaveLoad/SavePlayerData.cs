using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SavePlayerData
{
    readonly static string SAVE_FILE_PATH = Application.persistentDataPath + "/player.dt";

    public static void Save(Player player, PlayerInventory inventory)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(SAVE_FILE_PATH, FileMode.Create);

        PlayerData playerData = new PlayerData(player, inventory);

        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }
}
