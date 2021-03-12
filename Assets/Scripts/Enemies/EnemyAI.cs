using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    protected bool phase2;
    [SerializeField]
    private NavMeshAgent nav;

    private State _currentState;

    private void Start()
    {
        SetState(new StandByState(this));
    }

    private void Update()
    {
        _currentState.Tick();
    }

    public void SetState(State state)
    {
        _currentState?.OnStateExit();

        _currentState = state;

        _currentState?.OnStateEnter();
    }

    public void Move()
    {
    }
}
