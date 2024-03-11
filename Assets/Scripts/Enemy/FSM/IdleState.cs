using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    Alien alien;

    public IdleState(Alien alien)
    {
        this.alien = alien;
    }

    public void Enter()
    {
        alien.GetComponent<EnemyMovement>().StopMovement();
    }

    public void Exit()
    {

    }

    public void Update()
    {
        alien.GetComponent<EnemyMovement>().AvoidFromAsteroids();
    }
}
