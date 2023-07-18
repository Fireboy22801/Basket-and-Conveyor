using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

    void Update()
    {
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            if (onBelt[i] != null)
                onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction;
            else
                onBelt.RemoveAt(i);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}