using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, Ally
{
    //public Gun gun;

    public float health;
    public float maxHealth;
    public Slider healthBar;
    
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
    }
    
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        healthBar.value = health;
    }
}
