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
        //Attack the target if it is in range and cooldown had ended
        if (ai.TargetInRange())
        {
            if (Time.time >= ai.nextAttack)
            {
                ai.Action();
            }
        }
        //Otherwise, switch to move state
        else
        {
            ai.SetState(new MoveState(ai));
        }
    }
}
