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
        ai.Move();
    }
}
