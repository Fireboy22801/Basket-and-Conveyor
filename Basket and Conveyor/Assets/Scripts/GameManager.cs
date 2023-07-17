using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform handIKTarget;
    [SerializeField] private Transform Basket;
    [SerializeField] private HandCollider hand;
    [SerializeField] private Vector3 foodOfSet;
    [SerializeField] private QuestGenerator questGenerator;

    public float speed = 1.0f;

    private Animator animator;
    private AnimRig animRig;
    private Food food;

    private int[] collectedQuantities = new int[10];

    private bool shouldFollow = false;
    private bool hasFoodInHand = false;

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
            if (!hasFoodInHand)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Food"))
                    {
                        if (food != null)
                            shouldFollow = false;

                        food = hit.collider.GetComponent<Food>();

                        animRig.FollowFood(true);

                        shouldFollow = true;

                        StartCoroutine(UpdateHandIKTargetPosition());
                    }
                }
            }
        }
    }

    IEnumerator UpdateHandIKTargetPosition()
    {
        while (shouldFollow)
        {
            handIKTarget.position = food.transform.position;
            yield return null;
        }
    }

    public void TakeFood()
    {
        hasFoodInHand = true;
        food.tag = "Untagged";

        food.GetComponent<Rigidbody>().isKinematic = true;
        food.transform.SetParent(animRig.rigs[2].transform);
        food.transform.localPosition = Vector3.zero;

        animRig.Put(true);

        CollectFruit(food.fruitIndex);
    }


    public void DropFood()
    {
        shouldFollow = false;

        food.transform.SetParent(Basket);
        food.transform.localPosition = Vector3.zero;
        food = null;

        animRig.Idle();

        animator.Play("Idle");

        hasFoodInHand = false;
    }

    public void CollectFruit(int fruitIndex)
    {
        collectedQuantities[fruitIndex]++;
        questGenerator.CheckQuestCompletion(collectedQuantities);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
