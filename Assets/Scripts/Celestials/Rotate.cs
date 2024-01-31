using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Rotate : MonoBehaviour
{
    GameObject[] celestialBodies;

    [SerializeField]
    private float rotationRatio;


    // Start is called before the first frame update
    void Start()
    {
        celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        RotateAroundYAxis();
    }

    void RotateAroundYAxis()
    {
        foreach (GameObject target in celestialBodies)
        {

            float secs = 86400.0f;
            float degree = 0.0f;

            if (secs != 0)
                degree = 360 / secs * target.GetComponent<Rotate>().rotationRatio;

            if (target != null)
                target.GetComponent<Rigidbody>().transform.Rotate(0.0f, degree * Time.deltaTime, 0.0f);
        }
    }
}
