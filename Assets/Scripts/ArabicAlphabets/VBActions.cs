using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class VBActions : MonoBehaviour, IVirtualButtonEventHandler {
    public GameObject streamVideo;

    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (!streamVideo.activeSelf)
            streamVideo.SetActive(true);
        else
            streamVideo.GetComponent<VideoPlayer>().Play();
        print("Pressed");
        
    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonBehaviour vb)
    {
        streamVideo.GetComponent<VideoPlayer>().Pause();
        print("Released");
    }

    // Use this for initialization
    void Start () {
        this.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
	}
}
