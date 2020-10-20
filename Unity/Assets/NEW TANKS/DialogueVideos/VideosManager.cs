using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideosManager : MonoBehaviour
{
    private VideoPlayer _videoplayer;
    public ManagerScenes _daddyManager;

    public VideoClip AsukaClip;
    public VideoClip ShinjiClip;
    public VideoClip ReiClip;


    // Start is called before the first frame update
    void Start()
    {
        _videoplayer = GetComponent<VideoPlayer>();
        _daddyManager = GameObject.Find("DaddyManager").GetComponent<ManagerScenes>();
        PutCorrectVideo();
    }

    // Update is called once per frame
    void Update()
    {
        if (_videoplayer.isPaused)
        {
            SceneManager.LoadScene("MainPlayGame");
        }
    }

    private void PutCorrectVideo()
    {
        switch (_daddyManager.EVAChoose)
        {
            case (1):
                _videoplayer.clip = ReiClip;
                break;
            case (2):
                _videoplayer.clip = ShinjiClip;
                break;
            case (3):
                _videoplayer.clip = AsukaClip;
                break;
        }
    }

}
