using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVALookAt : MonoBehaviour
{
    public GameObject EVATarget;
    public string EVAOtherName;

    // Start is called before the first frame update
    void Start()
    {
        EVATarget=GameObject.Find(EVAOtherName);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = EVATarget.GetComponent<Transform>().position;
    }
}
