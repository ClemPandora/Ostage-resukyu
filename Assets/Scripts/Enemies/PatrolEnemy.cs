using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyAI
{
    [SerializeField]
    private GameObject _mine;

    private GameObject _myMine;

    public float callRange = 10;
    public float fleeDistance = 5;

    public override void Action()
    {
        //Set the next attack timer
        nextAttack = Time.time + attackCooldown;
        
        //Set a mine if the game is in phase 2 and it has not already set one
        if (phase2 && _myMine == null)
        {
            _myMine = Instantiate(_mine, transform.position + new Vector3(0, -1.5f, 0), transform.rotation);
        }
        //For each nearby enemies
        foreach (var coll in Physics.OverlapSphere(transform.position, callRange))
        {
            EnemyAI enemy = coll.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                //Set enemy target to the patrol target
                enemy.target = target;
                //Switch enemy state to move state
                enemy.SetState(new MoveState(enemy));
            }
        }
        //Switch to move state
        SetState(new MoveState(this));
    }

    //Detecte nearby ally and switch to action state
    public override void Detect()
    {
        foreach (var coll in Physics.OverlapSphere(transform.position, detectionRange, allyLayer))
        {
            if (!Physics.Linecast(transform.position, coll.transform.position, coverLayer))
            {
                target = coll.transform;
                SetState(new ActionState(this));
            }
        }
    }

    //Flee the target and switch to standby state if patrol is far enough
    public override void Move()
    {
        nav.SetDestination(transform.position + (transform.position - target.position).normalized * fleeDistance);
        if (!TargetInRange())
        {
            SetState(new StandByState(this));
        }
    }

    //Display the range where enemies will be called to attack
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, callRange);
    }
}
