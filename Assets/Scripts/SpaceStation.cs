using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    [SerializeField] float initialVelocity;
    Transform passengersDeck;
    float rotationSpeed = 2.5f;

    void Start()
    {
        passengersDeck = transform.Find("module7");
    }

    void Update()
    {
        if (passengersDeck != null)
        {
            passengersDeck.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
    }

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * initialVelocity * Time.deltaTime);
    }
}
