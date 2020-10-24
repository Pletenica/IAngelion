using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerScenes : MonoBehaviour
{
    [Range(1,3)]
    public int EVAChoose=1;
    public static ManagerScenes instance;
    [Range(1, 2)]
    public int AngelChooseMovement=2;
    [Range(1, 2)]
    public int EVAChooseMovement=1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ButtonEVAChoose(int _num)
    {
        EVAChoose = _num++;
    }

    public void ButtonEVAChooseMovement(int _num)
    {
        EVAChooseMovement = _num;
    }

    public void ButtonAngelChooseMovement(int _num)
    {
        AngelChooseMovement = _num;
    }

    public void ChangeToPlayScene()
    {
        SceneManager.LoadScene("DialogueScene");
    }

    public void EvaSum()
    {
        if (EVAChoose == 3)
        {
            EVAChoose = 1;
        }
        else
        {
            EVAChoose++;
        }
    }

    public void EvaRest()
    {
        if (EVAChoose == 1)
        {
            EVAChoose = 3;
        }
        else
        {
            EVAChoose--;
        }
    }
}
