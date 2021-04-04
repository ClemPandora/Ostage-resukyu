using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public int dmg = 2;
    private void OnTriggerEnter(Collider other)
    {
        //Damage colliding allies
        other.GetComponent<Ally>().Damage(dmg);
    }
}