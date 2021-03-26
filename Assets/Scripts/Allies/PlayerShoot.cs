using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera camera;
    
    public Transform firePoint;
    
    public GameObject bulletPrefab;
    public GameObject gun;

    public float fireRate;
    public float power;

    private bool canShoot;
    
    void Start()
    {
        canShoot = true;
    }
    
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            firePoint.transform.LookAt(hit.point);
            var dir = Quaternion.LookRotation(hit.point - firePoint.position);
            
            
            if (Input.GetButton("Fire1"))
            {
                if (canShoot)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    bullet.transform.rotation = firePoint.transform.rotation;
                    bullet.GetComponent<Rigidbody>().AddForce(dir * Vector3.forward * power);
                    canShoot = false;
                    StartCoroutine(Reloading(fireRate));
                }
            }
        }
    }
    
    IEnumerator Reloading(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}

