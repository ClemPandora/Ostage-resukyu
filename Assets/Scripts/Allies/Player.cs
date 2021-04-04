using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, Ally
{
    public Transform gun;
    public GameObject actualGun;

    public float health;
    public float maxHealth;
    public Slider healthBar;
    public GameObject panelDead;

    public float ammo;
    public float maxAmmo;
    public TextMeshProUGUI ammoText;
    
    void Start()
    {
        // init max health/ammo values
        health = maxHealth;
        healthBar.maxValue = maxHealth;

        ammo = maxAmmo;
    }
    
    void Update()
    {
        // lock max health
        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        // synchronize ath with health value
        healthBar.value = health;

        // lock max ammo
        if (ammo >= maxAmmo)
        {
            ammo = maxAmmo;
        }

        // synchronize ath with ammo value
        ammoText.text = ammo.ToString() + " / " + maxAmmo.ToString();
    }
    
    // player taking damage function
    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            panelDead.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
