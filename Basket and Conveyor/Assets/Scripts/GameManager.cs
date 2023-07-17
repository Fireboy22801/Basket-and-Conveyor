using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform handIKTarget;
    [SerializeField] private Transform Basket;
    [SerializeField] private HandCollider hand;
    [SerializeField] private Vector3 foodOfSet;

    public float speed = 1.0f;

    private Animator animator;
    private AnimRig animRig;
    private Food food;

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
            if (food == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Food"))
                    {
                        food = hit.collider.GetComponent<Food>();

                        animRig.FollowFood(true);

                        StartCoroutine(UpdateHandIKTargetPosition());
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
        food.tag = "Untagged";
        triggerEntered = true;

        food.GetComponent<Rigidbody>().isKinematic = true;
        food.transform.SetParent(animRig.rigs[2].transform);
        food.transform.localPosition = Vector3.zero;

        animRig.Put(true);
    }


    public void DropFood()
    {
        triggerEntered = false;
        food.transform.SetParent(Basket);
        food.transform.localPosition = Vector3.zero; 
        food = null;

        animRig.Idle();

        animator.Play("Idle");
    }

}
