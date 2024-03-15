using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnvironmentData
{
    public float[][] positionOfCelestialBodies;
    public float[][] rotationOfCelestialBodies;

    public float[] spaceStationPosition;
    public float[] spaceStationRotation;
    public float[] spaceStationVelocity;

    public EnvironmentData(GameObject[] celestialBodies, GameObject spaceStation)
    {
        this.positionOfCelestialBodies = new float[celestialBodies.Length][];
        this.rotationOfCelestialBodies = new float[celestialBodies.Length][];

        for (int i = 0; i < celestialBodies.Length; i++)
        {
            positionOfCelestialBodies[i] = new float[]
            {
                celestialBodies[i].transform.position.x,
                celestialBodies[i].transform.position.y,
                celestialBodies[i].transform.position.z
            };

            rotationOfCelestialBodies[i] = new float[]
            {
                celestialBodies[i].transform.rotation.x,
                celestialBodies[i].transform.rotation.y,
                celestialBodies[i].transform.rotation.z,
                celestialBodies[i].transform.rotation.w
            };
        }

        this.spaceStationPosition = new float[]
        {
            spaceStation.transform.position.x,
            spaceStation.transform.position.y,
            spaceStation.transform.position.z
        };

        this.spaceStationRotation = new float[]
        {
            spaceStation.transform.rotation.x,
            spaceStation.transform.rotation.y,
            spaceStation.transform.rotation.z,
            spaceStation.transform.rotation.w
        };

        this.spaceStationVelocity = new float[]
        {
            spaceStation.GetComponent<Rigidbody>().velocity.x,
            spaceStation.GetComponent<Rigidbody>().velocity.y,
            spaceStation.GetComponent<Rigidbody>().velocity.z
        };
    }
}