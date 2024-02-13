using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    [SerializeField]
    float initialVelocity;

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * initialVelocity * Time.deltaTime);
    }
}
