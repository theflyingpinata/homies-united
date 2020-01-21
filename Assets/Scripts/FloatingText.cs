using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public Vector3 randomizedIntensity = new Vector3(0.5f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomizedIntensity.x, randomizedIntensity.x),
                                                Random.Range(-randomizedIntensity.y, randomizedIntensity.y),
                                                Random.Range(-randomizedIntensity.z, randomizedIntensity.z));

    }
}
