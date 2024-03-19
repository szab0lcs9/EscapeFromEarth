using Assets.Scripts.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] ParticleSystem[] engineExhausts;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float strafeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayExhaustFumesParticleOnMoving();
    }

    private void FixedUpdate()
    {
        MovePlayerForwardOrBackward();
        RotatePlayer();
        StrafeLeft();
        StrafeRight();
    }

    private void MovePlayerForwardOrBackward()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * verticalInput * movementSpeed * Time.deltaTime;
        rb.AddForce(movement);

    }

    private void RotatePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 rotation = new Vector3(0.0f, horizontalInput * rotationSpeed, 0.0f);
        rb.AddTorque(rotation);
    }

    private void StrafeLeft()
    {
        if (Input.GetKey("q"))
        {
            Vector3 strafe = new Vector3(-strafeSpeed * Time.deltaTime, 0.0f, 0.0f);
            rb.AddRelativeForce(strafe);
        }
    }

    private void StrafeRight()
    {
        if (Input.GetKey("e"))
        {
            Vector3 strafe = new Vector3(strafeSpeed * Time.deltaTime, 0.0f, 0.0f);
            rb.AddRelativeForce(strafe);
        }
    }


    private void PlayExhaustFumesParticleOnMoving()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput != 0)
        {
            for (int i = 0; i < engineExhausts.Length; i++)
            {
                engineExhausts[i].Emit(1);
                engineExhausts[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < engineExhausts.Length; i++)
            {
                engineExhausts[i].Stop();
            }
        }
    }

    internal void StopMovement()
    {
        Vector3 actualVelocity = rb.velocity;
        rb.AddForce(-actualVelocity);
        rb.velocity = Vector3.zero;
    }
}
