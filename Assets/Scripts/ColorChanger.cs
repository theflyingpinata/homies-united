using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Flashes the given color for the given time
    public void FlashColor(Color color, float time)
    {
        StartCoroutine(ChangeColor(color, time));
    }

    IEnumerator ChangeColor(Color color, float time)
    {
        Color startColor = spriteRenderer.color;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(time);
        spriteRenderer.color = Color.white; // startColor;
    }

}
