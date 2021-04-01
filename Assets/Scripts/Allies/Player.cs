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
    public GameObject panelDead;
    
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

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            panelDead.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }


    void Movements()
    {
        
    }
}
