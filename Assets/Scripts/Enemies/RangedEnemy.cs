using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyAI
{
    public GameObject bullet;
    public float bulletSpeed = 3000;
    public override void Action()
    {
        //Set next attack timer
        nextAttack = Time.time + attackCooldown;
        
        //Instantiate a bullet
        GameObject instance = Instantiate(bullet, transform.position, transform.rotation);
        //Fire the bullet toward target position
        instance.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * bulletSpeed);
        if (phase2)
        {
            //Double bullet damage in phase 2
            EnemyBullet bull = instance.GetComponent<EnemyBullet>();
            if (bull != null)
            {
                bull.dmg *= 2;
            }
        }
    }
}