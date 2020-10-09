using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScenes : MonoBehaviour
{
    [Range(1,3)]
    public int EVAChoose=1;
    public static ManagerScenes instance;

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
        EVAChoose = _num;
    }

    public void ChangeToPlayScene()
    {
        SceneManager.LoadScene("MainPlayGame");
    }
}
