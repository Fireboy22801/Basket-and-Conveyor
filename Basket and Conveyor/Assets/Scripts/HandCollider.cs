using UnityEngine;

public class HandCollider : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            gameManager.TakeFood();
        }
    }
}
