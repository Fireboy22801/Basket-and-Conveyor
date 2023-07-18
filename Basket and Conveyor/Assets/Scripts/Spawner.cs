using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform spawnPoint;

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
            timer = rand.Next(1, 4);
            Spawn(prefabs[rand.Next(0, prefabs.Length)]);
        }
    }

    private void Spawn(GameObject gameObject)
    {
        GameObject instance = Instantiate(gameObject, spawnPoint.position, Quaternion.identity);
        pool.Add(instance);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
