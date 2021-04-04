using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyAI ai;

    //Called on update
    public virtual void Tick(){}

    //Called on state enter
    public virtual void OnStateEnter(){}
    
    //Called on state exit
    public virtual void OnStateExit(){}

    public State(EnemyAI ai)
    {
        this.ai = ai;
    }
}
