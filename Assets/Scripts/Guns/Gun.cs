using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   public int remainingAmmo;
   [SerializeField]
   private int damage;
   [SerializeField]
   private float bulletSpeed;
   public int bulletsInMagazine;
   public int magazineSize;

   public void Shoot()
   {
   }

   public void Reload()
   {
   }
}
