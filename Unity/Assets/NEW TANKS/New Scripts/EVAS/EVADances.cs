using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EVADances : MonoBehaviour
{
    public Animator EVAAnimator;
    public Button[] DanceButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DancingPrueba();
    }

    private void FixedUpdate()
    {
        EVAAnimator.SetInteger("Dancing", 0);
    }

    void DancingPrueba()
    {
        if (Input.GetKeyDown(KeyCode.F1)) PlayDance(1);
        if (Input.GetKeyDown(KeyCode.F2)) PlayDance(2);
        if (Input.GetKeyDown(KeyCode.F3)) PlayDance(3);
        if (Input.GetKeyDown(KeyCode.F4)) PlayDance(4);
        if (Input.GetKeyDown(KeyCode.F5)) PlayDance(5);
    }

    void PlayDance(int dance_num)
    {
        EVAAnimator.SetInteger("Dancing", dance_num);
    }
}
