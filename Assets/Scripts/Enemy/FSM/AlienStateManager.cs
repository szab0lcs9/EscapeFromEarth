using Assets.Scripts.Enemy.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStateManager : MonoBehaviour
{
    Alien alien;
    Transform player;
    AlienStateMachine alienStateMachine;

    [SerializeField] float sightDistance = 3.0f;
    [SerializeField] float attackDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (alien != null)
        {
            alienStateMachine.Update();
            HandleStates();
        }

    }

    private void HandleStates()
    {
        float sqrDistance = CalculateSqrDistance(player, alien.transform);

        if (sqrDistance < sightDistance * sightDistance)
            alienStateMachine.TransitionTo(new ChaseState(alien));

        if (sqrDistance < attackDistance * attackDistance)
            alienStateMachine.TransitionTo(new AttackState(alien));
    }


    public float CalculateSqrDistance(Transform obj1, Transform obj2)
    {
        Vector3 distance = obj1.position - obj2.position;

        return distance.sqrMagnitude;
    }


    public void Initialize(Alien alien)
    {
        this.alien = alien;

        alienStateMachine = new AlienStateMachine(alien);
        alienStateMachine.Initialize(new IdleState(alien));
    }
}
