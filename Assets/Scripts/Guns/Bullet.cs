using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public float speed = 10000;
    
    private Rigidbody rb;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Destroy(gameObject, 5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
