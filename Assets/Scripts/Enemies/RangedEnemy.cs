using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyAI
{
    public GameObject bullet;
    public float bulletSpeed = 3000;
    public override void Action()
    {
        if (Vector3.Distance(transform.position, target.position) > positionRange
            || Physics.Linecast(transform.position, target.position, coverLayer))
        {
            SetState(new MoveState(this));
        }
        else
        {
            nextAttack = Time.time + attackCooldown;
            GameObject instance = Instantiate(bullet, transform.position, transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * bulletSpeed);
        }
    }
}
