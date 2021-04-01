using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void Use(Player player)
    {
        GetComponentInParent<ItemSpawner>().itemSpawned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Use(other.GetComponent<Player>());
            Destroy(gameObject);
        }
    }
}
