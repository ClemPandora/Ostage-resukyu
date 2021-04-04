using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    public int health = 3;
    protected bool phase2;
    public float detectionRange;
    public float attackRange;
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

    //Called when the player enter in phase 2
    public virtual void SwitchPhase()
    {
        phase2 = true;
        //Up enemy speed
        nav.speed += 1;
    }

    private void Update()
    {
        _currentState.Tick();
    }

    //Switch the enemy state
    public void SetState(State state)
    {
        _currentState?.OnStateExit();

        _currentState = state;

        _currentState?.OnStateEnter();
    }

    //Called on update in standby state, allow enemy to detect nearby ally
    public virtual void Detect()
    {
        //For each nearby ally
        foreach (var coll in Physics.OverlapSphere(transform.position, detectionRange, allyLayer))
        {
            //Check if we can see it by using a sphereCast.
            if (Physics.SphereCastAll(transform.position, 0.5f, coll.transform.position - transform.position,
                Vector3.Distance(transform.position, coll.transform.position), coverLayer).Length == 0)
            {
                //If it's the case, then set it as the actual target, and change to move state
                target = coll.transform;
                SetState(new MoveState(this));
            }
        }
    }

    //Compare the distance between each nearby ally, and change target to the closest
    public void CheckNearestTarget()
    {
        foreach (var coll in Physics.OverlapSphere(transform.position, detectionRange, allyLayer))
        {
            if (Physics.SphereCastAll(transform.position, 0.5f, coll.transform.position - transform.position,
                Vector3.Distance(transform.position, coll.transform.position), coverLayer).Length == 0)
            {
                if (Vector3.Distance(transform.position, target.position) >
                    Vector3.Distance(transform.position, coll.transform.position))
                {
                    target = coll.transform;
                }
            }
        }
    }

    //Called on update in move phase, allow enemy to move toward target
    public virtual void Move()
    {
        if (TargetInRange())
        {
            //Switch to action state if the target is in attack range
            SetState(new ActionState(this));
        }
        else
        {
            nav.SetDestination(target.position);
        }
    }

    //Called on update in action phase if the last attack cooldown has ended
    public virtual void Action()
    {
    }

    //Check if the target is in attack range and visible by the enemy
    public bool TargetInRange()
    {
        return !(Physics.SphereCastAll(transform.position, 0.5f, target.position - transform.position,
                     Vector3.Distance(transform.position, target.position), coverLayer).Length > 0
                 || Vector3.Distance(transform.position, target.position) > attackRange);
    }

    //Reduce enemy health by the amount of damage received
    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    //Display detection and attack range in editor
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
