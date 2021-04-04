using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsItems = new List<GameObject>();

    public bool itemSpawned;

    public Transform spawnPoint;

    public float cd;
    private float _actualcd;
    
    void Start()
    {
        _actualcd = cd;
       prefabsItems = prefabsItems.OrderByDescending(o => o.GetComponent<Item>().spawnRate).ToList();
    }
    
    void Update()
    {
        if (!itemSpawned)
        {
            _actualcd -= Time.deltaTime;

            if (_actualcd <= 0)
            {
                Roll();
                _actualcd = cd;
            }
        }
    }

    void SpawnItem(GameObject item)
    {
        Instantiate(item, spawnPoint);
        itemSpawned = true;
    }

    void Roll()
    {
        float die = Random.Range(0,100);
        Debug.Log(die);
        float deltaDie = 0;

        for (int i = 0; i < prefabsItems.Count; i++)
        {
            deltaDie += prefabsItems[i].GetComponent<Item>().spawnRate;
            if (deltaDie >= die)
            {
                SpawnItem(prefabsItems[i]);
                deltaDie = 0;
                break;
            }
        }
    }
}
