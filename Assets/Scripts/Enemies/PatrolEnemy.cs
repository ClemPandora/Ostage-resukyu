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
        nextAttack = Time.time + attackCooldown;
        if (phase2 && _myMine == null)
        {
            _myMine = Instantiate(_mine, transform.position + new Vector3(0, -1.5f, 0), transform.rotation);
        }
        foreach (var coll in Physics.OverlapSphere(transform.position, callRange))
        {
            EnemyAI enemy = coll.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.target = target;
                enemy.SetState(new MoveState(enemy));
            }
        }
        SetState(new MoveState(this));
    }

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

    public override void Move()
    {
        nav.SetDestination(transform.position + (transform.position - target.position).normalized * fleeDistance);
        if (!AllyInRange())
        {
            SetState(new StandByState(this));
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, callRange);
    }
}
