using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HostageFollowPlayer : MonoBehaviour
{
	public Transform targetPlayer;
	public int detectionRangePlayer;
	public LayerMask playerLayer;
	public int speed;
	
	private NavMeshAgent _nav;
	private void Start()
	{
		_nav = GetComponent<NavMeshAgent>();
		_nav.speed = speed;
	}

	private void Update()
	{
		_nav.SetDestination(targetPlayer.position); // Hostage go to the player 

		foreach (var coll in Physics.OverlapSphere(transform.position, detectionRangePlayer, playerLayer))
		{
			if (coll.gameObject.CompareTag("Player"))
			{
				_nav.SetDestination(Vector3.zero);
			}
		}
	}
 
	 private void OnDrawGizmos()
	 {
		 Gizmos.color = Color.red;
		 Gizmos.DrawWireSphere(transform.position, detectionRangePlayer);
	 }
}
