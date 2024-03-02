using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    Alien alien;
    GameObject player;
    Rigidbody rb;

    public ChaseState(Alien alien)
    {
        this.alien = alien;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = alien.GetComponent<Rigidbody>();
    }

    public void Exit()
    {
        alien.StopMovement();
    }

    public void Update()
    {
        alien.transform.LookAt(player.transform);
        alien.AvoidFromAsteroids();
        alien.MoveTowardsPlayer(player.transform);
    }
}
