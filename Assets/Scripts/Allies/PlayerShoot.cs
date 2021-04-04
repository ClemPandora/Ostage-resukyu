using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera camera;
    
    public Transform firePoint;
    
    public GameObject bulletPrefab;
    
    public float fireRate;
    public float power;

    private bool canShoot;
    private bool noAmmo;
    
    
    void Start()
    {
        canShoot = true;
    }
    
    void Update()
    {
        // Check remaining ammo
        if (GetComponentInParent<Player>().ammo <= 0) 
        {
            noAmmo = true;
        }
        else
        {
            noAmmo = false;
        }
        
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            // set direction of shoot
            firePoint.transform.LookAt(hit.point);
            var dir = Quaternion.LookRotation(hit.point - firePoint.position);
            
            // shoot system
            if (Input.GetButton("Fire1"))
            {
                if (canShoot && !noAmmo)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    bullet.transform.rotation = firePoint.transform.rotation;
                    GetComponentInParent<Player>().ammo--;
                    bullet.GetComponent<Rigidbody>().AddForce(dir * Vector3.forward * power);
                    canShoot = false;
                    StartCoroutine(Reloading(fireRate));
                }
            }
        }
    }
    
    // fire rate coroutine
    IEnumerator Reloading(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}

