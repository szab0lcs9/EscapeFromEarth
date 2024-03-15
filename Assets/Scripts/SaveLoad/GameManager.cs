using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player = FindObjectOfType<Player>();
    PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
    Alien alien = FindObjectOfType<Alien>();
    GameObject[] celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");
    GameObject spaceStation = GameObject.FindGameObjectWithTag("SpaceStation");

    GameData gameData;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void Save()
    {
        SaveGame.Save(player, inventory, alien, celestialBodies, spaceStation);
    }

    public void Load()
    {
        GameData gameData = LoadGame.Load();

        player.Health = gameData.PlayerData.health;
        player.Shield = gameData.PlayerData.shield;
        player.transform.position = new Vector3(
            gameData.PlayerData.postition[0],
            gameData.PlayerData.postition[1],
            gameData.PlayerData.postition[2]);
        player.transform.rotation = new Quaternion(
            gameData.PlayerData.rotation[0],
            gameData.PlayerData.rotation[1],
            gameData.PlayerData.rotation[2],
            gameData.PlayerData.rotation[3]);
        inventory.NumberOfGold = gameData.PlayerData.numberOfGold;

        alien.Health = gameData.EnemyData.health;
        alien.Shield = gameData.EnemyData.shield;
        alien.transform.position = new Vector3(
            gameData.EnemyData.position[0],
            gameData.EnemyData.position[1],
            gameData.EnemyData.position[2]);
        alien.transform.rotation = new Quaternion(
            gameData.EnemyData.rotation[0],
            gameData.EnemyData.rotation[1],
            gameData.EnemyData.rotation[2],
            gameData.EnemyData.rotation[3]);

        for (int i = 0; i < celestialBodies.Length; i++)
        {
            celestialBodies[i].transform.position = new Vector3(
                gameData.EnvironmentData.positionOfCelestialBodies[i][0],
                gameData.EnvironmentData.positionOfCelestialBodies[i][1],
                gameData.EnvironmentData.positionOfCelestialBodies[i][2]);

            celestialBodies[i].transform.rotation = new Quaternion(
                gameData.EnvironmentData.rotationOfCelestialBodies[i][0],
                gameData.EnvironmentData.rotationOfCelestialBodies[i][1],
                gameData.EnvironmentData.rotationOfCelestialBodies[i][2],
                gameData.EnvironmentData.rotationOfCelestialBodies[i][3]);
        }

        spaceStation.transform.position = new Vector3(
            gameData.EnvironmentData.spaceStationPosition[0],
            gameData.EnvironmentData.spaceStationPosition[1],
            gameData.EnvironmentData.spaceStationPosition[2]);

        spaceStation.transform.rotation = new Quaternion(
            gameData.EnvironmentData.spaceStationRotation[0],
            gameData.EnvironmentData.spaceStationRotation[1],
            gameData.EnvironmentData.spaceStationRotation[2],
            gameData.EnvironmentData.spaceStationRotation[3]);

        spaceStation.GetComponent<Rigidbody>().velocity = new Vector3(
            gameData.EnvironmentData.spaceStationVelocity[0],
            gameData.EnvironmentData.spaceStationVelocity[1],
            gameData.EnvironmentData.spaceStationVelocity[2]);
    }
}
