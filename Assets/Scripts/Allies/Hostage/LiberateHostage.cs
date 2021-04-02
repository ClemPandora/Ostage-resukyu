using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiberateHostage : MonoBehaviour
{
	public HostageFollowPlayer hostage;
	public UnityEvent phase2Event;

	private void Awake()
	{
		phase2Event = new UnityEvent();
	}

	private void OnTriggerEnter(Collider other) // To liberate the hostage
	{
		if (other.gameObject.GetComponent<PlayerMovements>()) 
		{ 
			phase2Event.Invoke();
			hostage.GetComponent<HostageFollowPlayer>().enabled = true; 
			hostage.transform.SetParent(null);
		}
	}
}
