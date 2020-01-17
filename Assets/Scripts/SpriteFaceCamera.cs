using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour
{
    public Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(new Vector3(theCamera.transform.position.x, 0, theCamera.transform.position.z));
        //transform.LookAt(Vector3.Lerp(new Vector3(theCamera.transform.position.x, 0, theCamera.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z), .2f), Vector3.up);
        //transform.rotation = Quaternion.Euler(0, theCamera.transform.rotation.eulerAngles.y, 0);
    }
}
