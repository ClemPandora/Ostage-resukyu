using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ally ally = other.GetComponent<Ally>();
        if (ally != null)
        {
            //ally.Damage();
        }

        if (other.GetComponent<EnemyAI>() == null)
        {
            Destroy(gameObject);
        }
    }
}
