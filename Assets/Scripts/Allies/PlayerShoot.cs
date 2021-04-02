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

    private bool _canShoot;
    private bool _noAmmo;
    
    
    void Start()
    {
        _canShoot = true;
    }
    
    void Update()
    {
        if (GetComponentInParent<Player>().ammo <= 0)
        {
            _noAmmo = true;
        }
        else
        {
            _noAmmo = false;
        }
        
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            firePoint.transform.LookAt(hit.point);
            var dir = Quaternion.LookRotation(hit.point - firePoint.position);
            
            
            if (Input.GetButton("Fire1"))
            {
                if (_canShoot && !_noAmmo)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    bullet.transform.rotation = firePoint.transform.rotation;
                    GetComponentInParent<Player>().ammo--;
                    bullet.GetComponent<Rigidbody>().AddForce(dir * Vector3.forward * power);
                    _canShoot = false;
                    StartCoroutine(Reloading(fireRate));
                }
            }
        }
    }
    
    IEnumerator Reloading(float time)
    {
        yield return new WaitForSeconds(time);
        _canShoot = true;
    }
}

