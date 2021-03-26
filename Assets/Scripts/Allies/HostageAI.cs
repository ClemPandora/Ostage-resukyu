using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HostageAI : MonoBehaviour, Ally
{
   private NavMeshAgent _nav;
   public int frameInterval = 10;
   public int faceEnemyFactor = 50; //For facing the player/enemies

   public List<GameObject> enemies;
   
   //Take cover/hide
   private Vector3 _randomPosition;
   private Vector3 _coverPoint;
   public float rangeRandPoint = 6f;
   public bool isHiding;
   public int detectionRange;
   //Go To Cover
   public LayerMask coverLayer; //Set the layer that should be used as cover
   private Vector3 _coverObj; // To store the cover objects positions
   public LayerMask visibleLayer; // To declare objects on objects on layer that might obstruct the view betwen AI and player;
   public LayerMask enemyLayer;

   private float _maxCovDist = 30f; // If distance to cover is greater than this, do something else
   public bool coverIsClose; // Is Cover in range ?
   public bool coverNotReached = true; // If true, Ai is not close enough to the cover object

   public float distToCoverPos = 1f;
   public float distToCoverObj = 20f;

   public float rangeDist = 15f;
   private bool _playerInRange;

   private int _testCoverPos = 15;

   private bool _enemyIsInMyVision;

   private Transform _target;
   //bool to find positions behind cover 
   private bool RandomPoint(Vector3 center, float rangeOfRandPoint, out Vector3 resultCover)
   {
      for (int i = 0; i < _testCoverPos; i++)
      {
         _randomPosition = center + Random.insideUnitSphere * rangeOfRandPoint;
        
         for (int j = 0; j < enemies.Count; j++)
         {
            Vector3 direction = enemies[j].transform.position - _randomPosition;
            
            RaycastHit hitTestCov;
         
            if (Physics.Raycast(_randomPosition, direction.normalized, out hitTestCov, rangeOfRandPoint, visibleLayer))
            {
               if (hitTestCov.collider.gameObject.layer == coverLayer)
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
                  _playerInRange = true;
               }
               else
               {
                  _playerInRange = false;
               }
            }
         }

         if (_playerInRange)
         {
            CheckCoverDist(); // Check if cover is close enough
            if (coverIsClose)
            {
               
               if (coverNotReached)
               {
                  _nav.SetDestination(_coverObj - transform.forward); // go the cover obj
                  FaceEnemy();
               }

               if (!coverNotReached)
               {
                  TakeCover();
                  FaceEnemy();
               }
            }

            if (!coverIsClose)
            {
               //Todo si l'obj est loin
            }
         }
      }
   }

   void FaceEnemy()
   {
      var closestDistance = (enemies[0].transform.position - transform.position).sqrMagnitude;
      var targetNumber = 0;
      for (int i = 1; i < enemies.Count; i++) {
         var thisDistance = (enemies[i].transform.position - transform.position).sqrMagnitude;
         if (thisDistance < closestDistance) {
            closestDistance = thisDistance;
            targetNumber = i;
         }
      }
      
      Vector3 direction = (enemies[targetNumber].transform.position - transform.position).normalized;
        
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * faceEnemyFactor);
   }

   private void OnDrawGizmos()
   {
      //Vector3 direction = (enemies[0].transform.position - transform.position).normalized;
      
      //Debug.DrawRay(transform.position, direction.normalized * rangeRandPoint, Color.red, 2);
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
               else if(coverDistance > distToCoverObj && !_enemyIsInMyVision)
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

   void TakeCover()
   {
      if (RandomPoint(transform.position, rangeRandPoint, out _coverPoint))
      {
         if (_nav.isActiveAndEnabled)
         {
            _nav.SetDestination(_coverPoint);
            
            if ((_coverPoint - transform.position).sqrMagnitude <= distToCoverPos)
            {
               isHiding = true;
            }
         }
      }
   }
}
