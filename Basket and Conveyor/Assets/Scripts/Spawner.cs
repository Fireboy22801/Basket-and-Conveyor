using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int timeToSpawn;


    private List<GameObject> pool = new List<GameObject>();
    private float timer;
    private System.Random rand = new System.Random();


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timeToSpawn;
            Spawn(prefabs[rand.Next(0, prefabs.Length)]);
            CheckIfCanDestroy();
        }
    }

    private void Spawn(GameObject gameObject)
    {
        GameObject instance = Instantiate(gameObject, transform.position, Quaternion.identity);
        pool.Add(instance);
    }


    private void CheckIfCanDestroy()
    {
        for (int i = pool.Count - 1; i >= 0; i--)
        {
            if (pool[i].transform.position.y < -5)
            {
                Destroy(pool[i]);
                pool.RemoveAt(i);
            }
        }
    }
}
