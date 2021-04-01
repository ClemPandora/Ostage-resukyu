using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberateHostage : MonoBehaviour
{
	public HostageFollowPlayer hostage;

	private void OnTriggerEnter(Collider other) // To liberate the hostage
	{
		if (other.gameObject.GetComponent<PlayerMovements>()) 
		{ 
			hostage.GetComponent<HostageAI>().enabled = true; 
			hostage.transform.SetParent(null);
		}
		if (other.gameObject.CompareTag("Player"))
		{
			hostage.enabled = true;
			hostage.transform.SetParent(null);
		}
	}
}
