using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    Alien alien;
    GameObject player;

    public ChaseState(Alien alien)
    {
        this.alien = alien;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {

    }
}
