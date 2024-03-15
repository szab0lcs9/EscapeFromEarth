using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadGame
{
    public static GameData Load()
    {
        PlayerData playerData = LoadPlayerData.Load();
        EnemyData enemyData = LoadEnemyData.Load();
        EnvironmentData environmentData = LoadEnvironmentData.Load();

        return new GameData
        {
            PlayerData = playerData,
            EnemyData = enemyData,
            EnvironmentData = environmentData
        };
    }
}
