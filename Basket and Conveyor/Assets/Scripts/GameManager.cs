using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform handIKTarget;
    [SerializeField] private HandCollider hand;
    [SerializeField] private Vector3 foodOfSet;

    public float speed = 1.0f;

    private Animator animator;
    private AnimRig animRig;
    private Food food;

    private bool hasFood = false;
    private bool triggerEntered = false;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        animRig = GetComponent<AnimRig>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!hasFood)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Food"))
                    {
                        food = hit.collider.GetComponent<Food>();

                        handIKTarget.position = food.transform.position;

                        handIKTarget.SetParent(food.transform);

                        animator.SetTrigger("GrabItem");

                        animRig.Follow(true);

                        //StartCoroutine(UpdateHandIKTargetPosition(food.transform));
                    }
                }
            }
        }
    }

    IEnumerator UpdateHandIKTargetPosition()
    {
        while (!triggerEntered)
        {
            handIKTarget.position = food.transform.position;
            yield return null;
        }
    }

    public void TakeFood()
    {
        /*food.tag = "Untagged";
        triggerEntered = true;
        hasFood = true;
        animator.SetTrigger("PutItemInBasket");
        StartCoroutine(FoodFollowingHand());*/
    }

    IEnumerator FoodFollowingHand()
    {
        if (food != null)
        {
            while (hasFood)
            {
                food.transform.position = hand.transform.position + foodOfSet;
                yield return null;
            }
        }
    }

    private IEnumerator DropFood()
    {
        hasFood = false;
        triggerEntered = false;
        food = null;

        yield return new WaitForSeconds(1);
        animator.SetTrigger("Idle");
    }

}
