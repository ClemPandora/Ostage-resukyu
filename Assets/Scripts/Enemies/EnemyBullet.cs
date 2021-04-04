using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int dmg = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        //Apply damage to allies
        other.GetComponent<Ally>()?.Damage(dmg);
        
        //Destroy the bullet, except when colliding with enemies
        if (other.GetComponent<EnemyAI>() == null)
        {
            Destroy(gameObject);
        }
    }
}
