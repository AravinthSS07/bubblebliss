using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoButtonController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public GameObject targetButton;     // Reference to the Button component
    public GameObject videoPlayerObject;

    void Start()
    {
        // Ensure the button is initially disabled
        //targetButton.interactable = false;
        targetButton.SetActive(false);
        // Subscribe to the VideoPlayer's loopPointReached event
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Enable the button when the video ends
        //targetButton.interactable = true;
        videoPlayerObject.SetActive(false);
        targetButton.SetActive(true);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the script is destroyed
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}
