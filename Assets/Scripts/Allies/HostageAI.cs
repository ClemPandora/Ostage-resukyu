using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HostageAI : MonoBehaviour
{
   [Header("FaceEnemy")]
   public int frameInterval;
   public int faceEnemyFactor; //For facing the player/enemies
   
   [Header("Enemies")]
   public List<GameObject> enemies;

   [Header("Layer")]
   //Go To Cover
   public LayerMask coverLayer; //Set the layer that should be used as cover
   public LayerMask visibleLayer; // To declare objects on objects on layer that might obstruct the view betwen AI and player;
   public LayerMask playerLayer; // A layer for the player
   
   [Header("IsCovered")]
   public bool coverIsClose; // Is Cover in range ?
   public bool coverNotReached = true; // If true, Ai is not close enough to the cover object
   public bool isHiding;
   
   [Header("Distance")]
   public float distToCoverPos;
   public float distToCoverObj;
   public float rangeRandPoint;
   public float rangeDist;
   public int detectionRangePlayer;
   
   [Header("Player")] 
   public Transform targetPlayer;
   
   private bool _enemyInRange;
   private bool _enemyIsInMyVision;
   
   private int _coverPos = 15;
   
   private Vector3 _coverObj; // To store the cover objects positions
   private Vector3 _randomPosition; // Take cover/hide
   private Vector3 _coverPoint;
   
   private float _maxCovDist = 30f; 
   
   private NavMeshAgent _nav;
   
   //bool to find positions behind cover 
   private bool RandomPoint(Vector3 center, float rangeOfRandPoint, out Vector3 resultCover) // Find a random point behind an object 
   {
      for (int i = 0; i < _coverPos; i++)
      {
         _randomPosition = center + Random.insideUnitSphere * rangeOfRandPoint;
        
         for (int j = 0; j < enemies.Count; j++)
         {
            Vector3 direction = enemies[j].transform.position - _randomPosition;
            
            RaycastHit hitCov;
         
            if (Physics.Raycast(_randomPosition, direction.normalized, out hitCov, rangeOfRandPoint, visibleLayer))
            {
               if (hitCov.collider.gameObject.layer == coverLayer) // If the object is a cover object, the hostage can be hide
               {
                  resultCover = _randomPosition;
                  return true;
               }
            }
         }
      }
      resultCover = Vector3.zero;
      return false;
   }

   private void Start()
   {
      _nav = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
      if (_nav.isActiveAndEnabled)
      {
         if (Time.frameCount % frameInterval == 0) // to this only every few frames, no need to do it every frame
         {
            for (int i = 0; i < enemies.Count; i++)
            {
               float distance = (enemies[i].transform.position - transform.position).sqrMagnitude; // check distance to player
               
               if (distance < rangeDist)
               {
                  _enemyInRange = true;
                  Debug.Log("In range");
               }
               else if (distance > rangeDist)
               {
                 /* _nav.SetDestination(targetPlayer.position); // Hostage go to the player 
                  foreach (var coll in Physics.OverlapSphere(transform.position, detectionRangePlayer, playerLayer))
                  {
                     if (coll.gameObject.CompareTag("Player"))
                     {
                        _nav.SetDestination(Vector3.zero);
                     }
                  }*/
                  //_enemyInRange = false;
                  Debug.Log("Not in range");
               }
            }
         }

         if (_enemyInRange)
         {
            CheckCoverDist(); // Check if cover is close enough
            
            if (coverIsClose)
            {
               if (coverNotReached)
               {
                  _nav.SetDestination(_coverObj - transform.forward); // go the cover obj
                  FaceEnemy();
               }
               else if (!coverNotReached)
               {
                  TakeCover();
                  FaceEnemy();
               }
            }
         }
      }
   }

  /* private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, detectionRangePlayer);
   }
*/
   
  
   void FaceEnemy() // Look the enemy, he looks the nearest enemy
   {
      var closestDistance = (enemies[0].transform.position - transform.position).sqrMagnitude;
      var targetNumber = 0;
      for (int i = 1; i < enemies.Count; i++) // Check the nearest enemy in the list
      {
         var thisDistance = (enemies[i].transform.position - transform.position).sqrMagnitude;
         
         if (thisDistance < closestDistance) 
         {
            closestDistance = thisDistance;
            targetNumber = i;
         }
      }
      
      Vector3 direction = (enemies[targetNumber].transform.position - transform.position).normalized;
        
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Look in the direction of the nearest enemy
        
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * faceEnemyFactor);
   }

   void CheckCoverDist()
   {
      // Check if cover is in vicinity 
      Collider[] collides = Physics.OverlapSphere(transform.position, _maxCovDist, coverLayer);
      
      float minSqrDistance = Mathf.Infinity;

      Vector3 aiPosition = transform.position;

      for (int i = 0; i < collides.Length; i++)
      {
         float sqrDistanceToCenter = (aiPosition - collides[i].transform.position).sqrMagnitude;

         if (sqrDistanceToCenter < minSqrDistance)
         {
            minSqrDistance = sqrDistanceToCenter;
            var nearestCollider = collides[i];
            
            // To check if AI is already close enough to take cover
            float coverDistance = (nearestCollider.transform.position - aiPosition).sqrMagnitude;

            if (coverDistance <= _maxCovDist)
            {
               coverIsClose = true;
               _coverObj = nearestCollider.transform.position;
               
               if (coverDistance <= distToCoverObj && _enemyIsInMyVision)
               {
                  coverNotReached = false;
               }
               else if(coverDistance > distToCoverObj && !_enemyIsInMyVision) // For the hostage to replace himself in an update method
               {
                  coverNotReached = true;
               }
            }

            if (coverDistance >= _maxCovDist)
            {
               coverIsClose = false;
            }
         }
      }

      if (collides.Length < 1)
      {
         coverIsClose = false;
      }
   }

   void TakeCover() // Hostage take cover
   {
      if (RandomPoint(transform.position, rangeRandPoint, out _coverPoint))
      {
         if (_nav.isActiveAndEnabled)
         {
            _nav.SetDestination(_coverPoint); // Hostage go to the cover point
            
            if ((_coverPoint - transform.position).sqrMagnitude <= distToCoverPos)
            {
               isHiding = true;
            }
         }
      }
   }
}
