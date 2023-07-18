using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform handIKTarget;
    [SerializeField] private Transform Basket;
    [SerializeField] private HandCollider hand;
    [SerializeField] private GameObject questGenerator;
    [SerializeField] private GameObject conveyor;

    [Header("UI")]
    [SerializeField] private GameObject levelPassedText;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject floatingTextPrefab;

    [SerializeField] private GameObject dropFoodeffect;

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
    }


    public void DropFood()
    {
        GameObject effect = Instantiate(dropFoodeffect, Basket.transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        shouldFollow = false;

        food.transform.SetParent(Basket);
        food.transform.localPosition = Vector3.zero;

        AddPoint(food.transform);
        CollectFruit(food.fruitIndex);

        food = null;

        animRig.Idle();

        animator.Play("Idle");

        hasFoodInHand = false;
    }

    public void CollectFruit(int fruitIndex)
    {
        collectedQuantities[fruitIndex]++;
        questGenerator.GetComponent<QuestGenerator>().CheckQuestCompletion(collectedQuantities);
    }

    public void GameOver()
    {
        conveyor.SetActive(false);
        questGenerator.SetActive(false);

        animator.SetTrigger("Dance");

        levelPassedText.SetActive(true);
        nextLevelButton.SetActive(true);
    }

    private void AddPoint(Transform transform)
    {
        GameObject floatingText = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        floatingText.GetComponent<TextMeshPro>().text = "+1";
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
