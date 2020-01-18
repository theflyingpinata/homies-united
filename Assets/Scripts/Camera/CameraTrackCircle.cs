using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to make the camera rotate around a center with a specific radius, while looking at the center
public class CameraTrackCircle : MonoBehaviour
{
    public Vector3 center;
    public float radius;
    public float speed;
    public float currentAngle;

    // Update is called once per frame
    void Update()
    {
        currentAngle += speed * Time.deltaTime;
        if (currentAngle > 360)
            currentAngle -= 360;
        transform.position = center + new Vector3(radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad), center.y, radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad));
        transform.rotation = Quaternion.Euler(transform.rotation.x, currentAngle + 180f, transform.rotation.z);
    }
}
