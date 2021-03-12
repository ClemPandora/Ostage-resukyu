using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyAI ai;

    public virtual void Tick(){}

    public virtual void OnStateEnter(){}
    
    public virtual void OnStateExit(){}

    public State(EnemyAI ai)
    {
        this.ai = ai;
    }
}
