using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Food food;

    [SerializeField] private Transform handIKTarget;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Vector3 foodOfSet;

    public float speed = 1.0f;
    public Animator animator;

    private bool hasFood = false;
    private bool triggerEntered = false;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
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
                        animator.SetTrigger("GrabItem");
                        StartCoroutine(UpdateHandIKTargetPosition(food.transform));
                    }
                }
            }
        }
    }

    IEnumerator UpdateHandIKTargetPosition(Transform target)
    {
        while (!triggerEntered)
        {
            handIKTarget.position = target.position;
            yield return null;
        }
    }

    public void TakeFood()
    {
        food.tag = "Untagged";
        triggerEntered = true;
        hasFood = true;
        animator.SetTrigger("PutItemInBasket");
        StartCoroutine(FoodFollowingHand());
    }

    IEnumerator FoodFollowingHand()
    {
        if (food != null)
        {
            while (hasFood)
            {
                food.transform.position = rightHand.position + foodOfSet;
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
