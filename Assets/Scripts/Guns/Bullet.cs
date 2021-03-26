using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public float speed = 10000;
    
    private Rigidbody rb;
    //private Vector3 eulerAngleVelocity;
    
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
       // eulerAngleVelocity = new Vector3(0, speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
