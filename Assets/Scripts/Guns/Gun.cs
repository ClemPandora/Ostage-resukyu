using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
   public int maxAmmo;

   public float fireRate;
   
   
   [SerializeField]
   private int damage;

   [SerializeField]
   private bool holded;

   [SerializeField] 
   private Quaternion baseRot;
   
   
   public override void Use(Player player)
   {
      Destroy(player.actualGun);
      GetComponentInParent<ItemSpawner>().itemSpawned = false;
      gameObject.GetComponent<Collider>().enabled = false;
      gameObject.transform.SetParent(player.gun);
      gameObject.transform.position = player.gun.position;
      player.actualGun = gameObject;
      gameObject.transform.localRotation = baseRot;
      player.maxAmmo = maxAmmo;
      player.GetComponent<PlayerShoot>().bulletPrefab.GetComponent<Bullet>().dmg = damage;
      player.GetComponent<PlayerShoot>().fireRate = fireRate;
      player.ammo = maxAmmo;
      
   }
   
   public void Shoot()
   {
   }

   public void Reload()
   {
   }
   
}