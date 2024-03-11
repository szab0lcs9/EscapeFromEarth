using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    Alien alien;
    GameObject player;

    public AttackState(Alien alien)
    {
        this.alien = alien;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Exit()
    {
        alien.StopCoroutine("MissileLaunch");
    }

    public void Update()
    {
        alien.transform.LookAt(player.transform);   // TODO: szebben megoldani a mozgást!!
        alien.GetComponent<EnemyMovement>().AvoidFromAsteroids();
        alien.Attack();
    }
}
