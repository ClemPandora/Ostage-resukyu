using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyAI
{
    public GameObject bullet;
    public float bulletSpeed = 3000;
    public override void Action()
    {
        nextAttack = Time.time + attackCooldown;
        GameObject instance = Instantiate(bullet, transform.position, transform.rotation);
        instance.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * bulletSpeed);
    }
}