using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageLife : MonoBehaviour, Ally
{
    public int life, waitForSeconds;
    public HostageAI hostageAi;
    public HostageFollowPlayer hostageFollowPlayer;
    
    public void Damage(int dmg)
    {
        life -= dmg;
        hostageFollowPlayer.enabled = false;
        hostageAi.enabled = true;
        StartCoroutine(WaitForFollowPlayerAgain());
    }

    private void Update()
    {
        if (life <= 0)
        {
            //TODO the death of hostage
        }

        if (Input.GetKey(KeyCode.A))
        {
            Damage(1);
        }
    }

    IEnumerator WaitForFollowPlayerAgain()
    {
        yield return new WaitForSeconds(waitForSeconds);
        hostageFollowPlayer.enabled = true;
        hostageAi.enabled = false;
    }
}

