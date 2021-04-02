using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public int dmg = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ally>() != null)
        {
            other.GetComponent<Ally>()?.Damage(dmg);
            Destroy(gameObject);
        }
    }
}
