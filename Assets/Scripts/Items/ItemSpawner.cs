using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsItems = new GameObject[3];

    public bool itemSpawned;

    public Transform spawnPoint;

    public float cd;
    private float _actualcd;
    
    void Start()
    {
        _actualcd = cd;
    }
    
    void Update()
    {
        if (!itemSpawned)
        {
            _actualcd -= Time.deltaTime;

            if (_actualcd <= 0)
            {
                SpawnItem(prefabsItems[Random.Range(0,prefabsItems.Length-1)]);
                _actualcd = cd;
                itemSpawned = true;
            }
        }
    }

    void SpawnItem(GameObject item)
    {
        Instantiate(item, spawnPoint);
    }
}
