using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    protected bool phase2;
    public float detectionRange;
    public float positionRange;
    [SerializeField]
    private NavMeshAgent nav;
    public Transform target;
    public LayerMask allyLayer;
    public LayerMask coverLayer;
    private State _currentState;
    public float attackCooldown;
    [HideInInspector]
    public float nextAttack;

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
        Debug.Log(state);

        _currentState?.OnStateEnter();
    }

    public virtual void Detect()
    {
        foreach (var coll in Physics.OverlapSphere(transform.position, detectionRange, allyLayer))
        {
            if (!Physics.Linecast(transform.position, coll.transform.position, coverLayer))
            {
                target = coll.transform;
                SetState(new MoveState(this));
            }
        }
    }

    public virtual void Move()
    {
        if (Vector3.Distance(transform.position, target.position) <= positionRange
            && !Physics.Linecast(transform.position, target.position, coverLayer))
        {
            nav.SetDestination(transform.position);
            SetState(new ActionState(this));
        }
        else
        {
            nav.SetDestination(target.position);
        }
    }

    public virtual void Action()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, positionRange);
    }
}
