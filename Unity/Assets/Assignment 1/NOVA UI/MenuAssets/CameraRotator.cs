using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float cameraspeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, cameraspeed * Time.deltaTime, 0);
    }
}
