using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int speed = 3;
    public Vector3 desiredPosition;
    public bool isTaken = false;
    public bool isOnBasket = false;

    private void Update()
    {
        if (isTaken)
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * speed);

        if(Input.GetKeyDown(KeyCode.Space))
            isOnBasket = true;
    }
}
