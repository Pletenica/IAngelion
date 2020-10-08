using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiToCamera : MonoBehaviour
{
    private Camera camera;
    public Transform forwardpos;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(forwardpos, Vector3.forward);
        //transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }
}
