using TMPro;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private System.Random rand = new System.Random();

    private string[] products = new string[] { "Apple", "Avocado", "Banana", "Cherrie", "Lemon",
        "Peach", "Peanut", "Pear", "Strawberry", "Watermelon"};

    private int quantity;
    private string product;

    private int questProductIndex;

    private void Start()
    {
        GenerateQuest();
    }

    private void GenerateQuest()
    {
        questProductIndex = rand.Next(products.Length);
        product = products[questProductIndex];
        quantity = rand.Next(1, 2);

        text.text = $"Collect {quantity} {product}" + (quantity > 1 ? "s" : "");
    }

    public void CheckQuestCompletion(int[] collectedQuantities)
    {
        bool questCompleted = true;

        if (collectedQuantities[questProductIndex] < quantity)
        {
            questCompleted = false;
        }

        if (questCompleted)
        {
            GameManager.instance.GameOver();
        }
    }
}
