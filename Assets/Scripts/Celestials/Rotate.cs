using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    private float rotationRatio;


    // Start is called before the first frame update
    void Start()
    {

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
        float secs = 86400.0f;
        float angle = 0.0f;

        if (secs != 0)
            angle = 360 / secs * rotationRatio;

        prefab.GetComponent<Transform>().Rotate(Vector3.up, angle * Time.deltaTime);            
    }
}
