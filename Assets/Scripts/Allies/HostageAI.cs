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
   public int facePlayerFactor = 50; //For facing the player/enemies

   public List<GameObject> enemies;
   
   //Take cover/hide
   private Vector3 _randomPosition;
   private Vector3 _coverPoint;
   public float rangeRandPoint = 6f;
   public bool isHiding;
   
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
                  FacePlayer();
               }

               if (!coverNotReached)
               {
                  TakeCover();
                  FacePlayer();
               }
            }

            if (!coverIsClose)
            {
               //Todo si l'obj est loin
            }
         }
      }
   }

   void FacePlayer()
   {
      Vector3 direction = (enemies[0].transform.position - transform.position).normalized;
      
     /* foreach (var enemy in enemies)
      {
         RaycastHit hits; 
         if (Physics.Raycast(transform.position, enemy.transform.position, out hits, Mathf.Infinity, enemyLayer))
         {
            Debug.Log ($"I'm the otage and i see {hits.collider.gameObject.name}");
            Debug.DrawLine(transform.position, enemy.transform.position * 50, Color.green, 20, true);
         }
      } 
      */
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerFactor);
      RaycastHit hit;
         
      if (Physics.Raycast(_randomPosition, direction.normalized, out hit, rangeRandPoint, enemyLayer))
      {
         if (hit.collider.gameObject.CompareTag("Player"))
         {
            _enemyIsInMyVision = true;
            Debug.DrawRay(transform.position, direction.normalized, Color.red, 200);
            Debug.Log(hit.collider.gameObject.name);
         }
      }
      
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
