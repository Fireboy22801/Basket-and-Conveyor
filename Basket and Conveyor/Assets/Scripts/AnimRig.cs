using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimRig : MonoBehaviour
{
    public Rig rig;

    public float weightSpeed = 1f;

    private bool isWeightUp;
    private bool stop = false;

    private void Update()
    {
        if (!stop)
        {
            if (isWeightUp)
            {
                rig.weight = Mathf.MoveTowards(rig.weight, 1, weightSpeed * Time.deltaTime);
                if (rig.weight == 1)
                    stop = true;
            }
            else
            {
                rig.weight = Mathf.MoveTowards(rig.weight, 0, weightSpeed * Time.deltaTime);
                if (rig.weight == 0)
                    stop = true;
            }
        }
    }

    public void Follow(bool isFollow)
    {
        isWeightUp = isFollow;
        stop = false;
    }
}


