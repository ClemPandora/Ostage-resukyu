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
        ai.CheckNearestTarget();
        ai.Move();
    }

    public override void OnStateEnter()
    {
        ai.nav.isStopped = false;
    }

    public override void OnStateExit()
    {
        ai.nav.isStopped = true;
    }
}
