using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(EnemyAI ai) : base(ai)
    {
    }

    public override void Tick()
    {
        //Switch to nearest target if there is one
        ai.CheckNearestTarget();
        ai.Move();
    }

    public override void OnStateEnter()
    {
        //Enable nav mesh agent
        ai.nav.isStopped = false;
    }

    public override void OnStateExit()
    {
        //Stop nav mesh agent
        ai.nav.isStopped = true;
    }
}
