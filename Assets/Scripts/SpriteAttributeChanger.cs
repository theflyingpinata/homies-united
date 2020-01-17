using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAttributeChanger : MonoBehaviour
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
        spriteRenderer.color = color;
        yield return new WaitForSeconds(time);
        if(spriteRenderer.color == color)
            spriteRenderer.color = Color.white; // startColor;
    }

    public void SquashSprite(float time)
    {
        StartCoroutine(Squash(time));
    }

    IEnumerator Squash(float time)
    {
        gameObject.transform.localScale = new Vector3(1, .5f, 1);
        yield return new WaitForSeconds(time);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ChangeSprites(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

}
