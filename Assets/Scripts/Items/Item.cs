using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float spawnRate;
    
    public virtual void Use(Player player)
    {
        GetComponentInParent<ItemSpawner>().itemSpawned = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Use(other.GetComponent<Player>());
        }
    }
}
