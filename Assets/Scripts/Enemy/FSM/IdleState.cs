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
        alien.StopMovement();
    }

    public void Exit()
    {

    }

    public void Update()
    {
        alien.AvoidFromAsteroids();
    }
}
