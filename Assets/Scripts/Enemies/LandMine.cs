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
            //Damage colliding allies and destroy the mine
            other.GetComponent<Ally>()?.Damage(dmg);
            if (other.GetComponent<Player>() != null)
            {
                //Ensure that the mines is removed from the mine difuser
                other.GetComponent<Player>().mineDefuser.Explode(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
