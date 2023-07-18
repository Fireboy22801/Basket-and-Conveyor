using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float speed = 1f;
    public float duration = 1f;

    private float timeElapsed = 0f;
    private TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= duration)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1f, 0f, (timeElapsed - duration) / duration));

            if (timeElapsed >= duration * 2)
            {
                Destroy(gameObject);
            }
        }
    }
}
