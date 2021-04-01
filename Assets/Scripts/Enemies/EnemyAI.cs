﻿using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    public int health = 3;
    protected bool phase2;
    public float detectionRange;
    public float positionRange;
    [SerializeField]
    public NavMeshAgent nav;
    public Transform target;
    public LayerMask allyLayer;
    public LayerMask coverLayer;
    private State _currentState;
    public float attackCooldown;
    [HideInInspector]
    public float nextAttack;

    private void Start()
    {
        FindObjectOfType<LiberateHostage>().phase2Event.AddListener(SwitchPhase);
        SetState(new StandByState(this));
    }

    public virtual void SwitchPhase()
    {
        phase2 = true;
        nav.speed += 1;
    }

    private void Update()
    {
        _currentState.Tick();
    }

    public void SetState(State state)
    {
        _currentState?.OnStateExit();

        _currentState = state;
        Debug.Log(name +" : "+state);

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
        if (AllyInRange())
        {
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

    public bool AllyInRange()
    {
        return !(Physics.SphereCastAll(transform.position, 0.5f, target.position - transform.position,
                     Vector3.Distance(transform.position, target.position), coverLayer).Length > 0
                 || Vector3.Distance(transform.position, target.position) > positionRange);
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, positionRange);
    }
}
