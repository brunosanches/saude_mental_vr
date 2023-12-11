using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.EventSystems; // Required for Event System
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using Oculus.Voice.Windows;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI menuButtonText;
    public GameObject buttons;
    public VideoPlayer player;
    public VideoSwitcher videoSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        buttons.SetActive(false);
        menuButtonText.text = "Iniciar simulação";
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.activeSelf)
        {
            player.Pause();
        }
        else
        {
            player.Play();
        }

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            menuButtonText.text = "Reiniciar a simulação";
            Debug.Log("Menu Button pressed!");
            Debug.Log(menu.activeSelf);
            menu.SetActive(!menu.activeSelf);
            buttons.SetActive(!buttons.activeSelf);
            

            
        }
  
    }

    public void ResetCurrentVideoIndex()
    {
        VideoList videoList = videoSwitcher.GetVideoList();
        Debug.Log("Reset current video index");
        videoSwitcher.setCurrentVideoIndex(0);
        videoSwitcher.buttonTexts[0].text = videoList.videos[1].text;
        videoSwitcher.buttonTexts[1].text = videoList.videos[2].text;
        videoSwitcher.videoPlayer.url = videoList.videos[0].url;
        videoSwitcher.videoPlayer.Stop();
        videoSwitcher.videoPlayer.Play();
        menuButtonText.text = "Iniciar a simulação";
        menu.SetActive(false);
        buttons.SetActive(true);
    }

    public void EndSimulation()
    {

        VideoList videoList = videoSwitcher.GetVideoList();
        Debug.Log("EndSimulation");
        videoSwitcher.setCurrentVideoIndex(0);
        videoSwitcher.setIsInFeedback(true);
        menuButtonText.text = "Reiniciar a simulação";

        videoSwitcher.setIsInFinal(false);
        videoSwitcher.buttonTexts[0].text = videoList.feedbackVideos[0].option1;
        videoSwitcher.buttonTexts[1].text = videoList.feedbackVideos[0].option2;

        videoSwitcher.videoPlayer.url = videoList.feedbackVideos[0].url;

        videoSwitcher.videoPlayer.Stop();
        videoSwitcher.videoPlayer.Play();
        menu.SetActive(false);
        buttons.SetActive(true);
    }
}
