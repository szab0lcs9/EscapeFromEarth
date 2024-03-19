using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnvironmentalBehavior : MonoBehaviour
{
    GameObject[] celestialBodies;

    private readonly float G = 6.674f * Mathf.Pow(10f, -5f);

    [SerializeField]
    private float timeScale;


    // Start is called before the first frame update
    void Start()
    {
        timeScale = 1;
        celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");

        InitialVelocity();

        AudioManager.Instance.PlaySFX("SpaceBackgroundSound", looping: true);
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        foreach (GameObject orb1 in celestialBodies)
        {
            foreach (GameObject orb2 in celestialBodies)
            {
                if (!orb1.Equals(orb2))
                {
                    float m1 = orb1.GetComponent<Rigidbody>().mass;
                    float m2 = orb2.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(orb1.transform.position, orb2.transform.position);

                    orb1.GetComponent<Rigidbody>().AddForce((orb2.transform.position - orb1.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject orb1 in celestialBodies)
        {
            foreach (GameObject orb2 in celestialBodies)
            {
                if (!orb1.Equals(orb2))
                {
                    float m2 = orb2.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(orb1.transform.position, orb2.transform.position);

                    orb1.transform.LookAt(orb2.transform);

                    orb1.GetComponent<Rigidbody>().velocity += orb1.transform.right * Mathf.Sqrt((G * m2) / r);
                }
            }
        }
    }
}