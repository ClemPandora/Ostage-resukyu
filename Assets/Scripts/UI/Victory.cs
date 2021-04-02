using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
   public GameObject victoryPanel, canvasPlayer;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<HostageLife>())
      {
         victoryPanel.SetActive(true);
         canvasPlayer.SetActive(false);
         Time.timeScale = 0f;
      }
   }
}
