using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy.FSM
{
    [Serializable]
    public class AlienStateManager
    {
        public IState CurrentState { get; private set; }

        public IdleState idleState;
        public ChaseState chaseState;
        public AttackState attackState;

        public AlienStateManager(Alien alien)
        {
            this.idleState = new IdleState(alien);
            this.chaseState = new ChaseState(alien);
            this.attackState = new AttackState(alien);
        }

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }
}
