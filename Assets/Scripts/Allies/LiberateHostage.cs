using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberateHostage : MonoBehaviour
{
	public HostageAI hostage;

	private void OnTriggerEnter(Collider other)
	{
		//TODO when the player will be implemented
		/* if (other.gameObject.GetComponent<PlayerMovements>())
		 {
		     hostage.GetComponent<HostageAI>().enabled = true;
		     hostage.transform.SetParent(null);
		 }*/
		if (other.gameObject.CompareTag("Player"))
		{
			hostage.GetComponent<HostageAI>().enabled = true;
			hostage.transform.SetParent(null);
		}
	}
}
