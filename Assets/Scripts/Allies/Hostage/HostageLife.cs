using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HostageLife : MonoBehaviour, Ally
{
    public int life, waitForSeconds;
    public HostageAI hostageAi;
    public HostageFollowPlayer hostageFollowPlayer;
    public GameObject panelDead;
    public Slider healthBarHostage;
    private NavMeshAgent _nav;

    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
    }

    public void Damage(int dmg) // Damage the player
    {
        life -= dmg;
        healthBarHostage.value = life;
        hostageFollowPlayer.enabled = false;
        hostageAi.enabled = true;
        _nav.speed = 7;
        if (life <= 0)
        {
            Time.timeScale = 0f;
            panelDead.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        StartCoroutine(WaitForFollowPlayerAgain());
    }

    IEnumerator WaitForFollowPlayerAgain() // Wait for follow the player again 
    {
        yield return new WaitForSeconds(waitForSeconds);
        _nav.speed = 3;
        hostageFollowPlayer.enabled = true;
        hostageAi.enabled = false;
    }
}

