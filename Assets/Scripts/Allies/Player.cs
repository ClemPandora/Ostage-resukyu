using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, Ally
{
    //public Gun gun;

    public float health;
    public float maxHealth;
    public Slider healthBar;
    public GameObject panelDead;

    public float ammo;
    public float maxAmmo;
    public TextMeshProUGUI ammoText;
    
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;

        ammo = maxAmmo;
    }
    
    void Update()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        healthBar.value = health;

        if (ammo >= maxAmmo)
        {
            ammo = maxAmmo;
        }

        ammoText.text = ammo.ToString() + " / " + maxAmmo.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        
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
}
