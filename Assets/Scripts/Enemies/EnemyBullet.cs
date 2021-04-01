using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int dmg = 1;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Ally>()?.Damage(dmg);
        other.GetComponent<HostageLife>()?.Damage(dmg);
        if (other.GetComponent<EnemyAI>() == null)
        {
            Destroy(gameObject);
        }
    }
}
