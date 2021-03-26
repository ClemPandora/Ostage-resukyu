using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ally ally = other.GetComponent<Ally>();
        if (ally != null)
        {
            //Ally.Damage();
            Debug.Log("Damage");
        }
    }
}
