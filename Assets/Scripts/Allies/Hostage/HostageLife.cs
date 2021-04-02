using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostageLife : MonoBehaviour, Ally
{
    public int life, waitForSeconds;
    public HostageAI hostageAi;
    public HostageFollowPlayer hostageFollowPlayer;
    public GameObject panelDead;
    public Slider healthBarHostage;
    
    public void Damage(int dmg) // Damage the player
    {
        life -= dmg;
        healthBarHostage.value = life;
        hostageFollowPlayer.enabled = false;
        hostageAi.enabled = true;
        if (life <= 0)
        {
            panelDead.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        StartCoroutine(WaitForFollowPlayerAgain());
    }

    IEnumerator WaitForFollowPlayerAgain() // Wait for follow the player again 
    {
        yield return new WaitForSeconds(waitForSeconds);
        hostageFollowPlayer.enabled = true;
        hostageAi.enabled = false;
    }
}

