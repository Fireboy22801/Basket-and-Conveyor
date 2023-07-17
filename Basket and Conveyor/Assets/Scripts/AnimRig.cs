using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimRig : MonoBehaviour
{
    public Rig[] rigs;

    public float weightSpeed = 1f;

    private GameManager gameManager;

    private bool isWeightUp;
    private bool stop = false;

    private int rigIndex;

    private bool isIdle;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (rigs[1].weight == 1)
            gameManager.DropFood();

        if (!stop)
        {
            if (!isIdle)
            {
                if (isWeightUp)
                {
                    rigs[rigIndex].weight = Mathf.MoveTowards(rigs[rigIndex].weight, 1, weightSpeed * Time.deltaTime);
                    if (rigs[rigIndex].weight == 1)
                        stop = true;
                }
                else
                {
                    rigs[rigIndex].weight = Mathf.MoveTowards(rigs[rigIndex].weight, 0, weightSpeed * Time.deltaTime);
                    if (rigs[rigIndex].weight == 0)
                        stop = true;
                }
            }
            else
            {
                for (rigIndex = 0; rigIndex < rigs.Length - 1; rigIndex++)
                    rigs[rigIndex].weight = Mathf.MoveTowards(rigs[rigIndex].weight, 0, weightSpeed * Time.deltaTime);

                if (rigs[rigIndex - 1].weight == 0)
                {
                    stop = true;
                    isIdle = false;
                }
            }

        }
    }

    public void FollowFood(bool isFollow)
    {
        isWeightUp = isFollow;
        stop = false;
        rigIndex = 0;
    }

    public void Put(bool isFollow)
    {
        isWeightUp = isFollow;
        stop = false;
        rigIndex = 1;
    }

    public void Idle()
    {
        stop = false;
        isIdle = true;
        isWeightUp = false;
    }
}


