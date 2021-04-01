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
        if (ai.AllyInRange())
        {
            if (Time.time >= ai.nextAttack)
            {
                ai.Action();
            }
        }
        else
        {
            ai.SetState(new MoveState(ai));
        }
        
    }
}
