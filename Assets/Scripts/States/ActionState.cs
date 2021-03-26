using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : State
{
    public ActionState(EnemyAI ai) : base(ai)
    {
    }

    public override void Tick()
    {
        if (Time.time >= ai.nextAttack)
        {
            ai.Action();
        }
    }
}
