using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    Alien alien;

    public AttackState(Alien alien)
    {
        this.alien = alien;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        alien.Attack();
    }
}
