using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg = 1;

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
        other.GetComponent<EnemyAI>()?.Damage(dmg);
        Destroy(gameObject);
    }
}
