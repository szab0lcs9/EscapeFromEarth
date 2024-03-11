using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    Alien alien;
    Transform player;

    public ChaseState(Alien alien)
    {
        this.alien = alien;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Exit()
    {
        alien.GetComponent<EnemyMovement>().StopMovement();
    }

    public void Update()
    {
        alien.transform.LookAt(player.transform); // TODO: szebben megoldani a mozgást!!
        alien.GetComponent<EnemyMovement>().AvoidFromAsteroids();
        alien.GetComponent<EnemyMovement>().MoveTowardsPlayer(player);
    }
}
