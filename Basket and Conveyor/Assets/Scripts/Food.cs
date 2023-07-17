using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isOnBasket = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            isOnBasket = true;
    }
}
