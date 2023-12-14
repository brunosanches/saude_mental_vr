using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine.UIElements;

[System.Serializable]
public class VideoData
{
    public string url;
    public string text;
    public bool is_final;
}

[System.Serializable]
public class FeedbackVideoData
{
    public string url;
    public string option1;
    public string option2;
    public bool is_final;
}

[System.Serializable]
public class VideoList
{
    public VideoData[] videos;
    public FeedbackVideoData[] feedbackVideos;
}



public class VideoSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<TextMeshProUGUI> buttonTexts;
    public List<ButtonController> buttonControllers;
    public GameObject buttons_canvas;
    private VideoList videoList;
    private int currentVideoIndex = 0;
    private bool is_in_final = false;
    private bool is_in_feedback = false;

    private void Start()
    {
        // Inicia o Download do arquivo json que definirá a sequência de vídeos
        Transform childTransform = transform.Find("JsonDownloader");
        JsonDownloader jsonDownloader = childTransform.GetComponent<JsonDownloader>();
        Debug.Log(jsonDownloader);
        jsonDownloader.onComplete += HandleJsonDownloaded;

        jsonDownloader.StartDownload();

        videoPlayer.isLooping = false;

    }

    public VideoList GetVideoList()
    {
        return videoList;
    }

    public int getCurrentVideoIndex()
    {
        return currentVideoIndex;
    }

    public void setCurrentVideoIndex(int videoIndex)
    {
        currentVideoIndex = videoIndex;
    }

    private void HandleJsonDownloaded(TextAsset jsonFile)
    {
        // Após realizar o download, inicia a execução de vídeos
        Debug.Log("Conteúdo do JSON: " + jsonFile.text);
        StartVideos(jsonFile);
    }

    private void StartVideos(TextAsset jsonFile)
    {
        string jsonString = jsonFile.text;
        videoList = JsonUtility.FromJson<VideoList>(jsonString);

        if (videoList.videos.Length > 0)
        {
            // Aqui você precisa modificar a lógica para trabalhar com URLs em vez de VideoClips
            videoPlayer.url = videoList.videos[currentVideoIndex].url;
            buttonTexts[0].text = videoList.videos[2 * currentVideoIndex + 1].text;
            buttonTexts[1].text = videoList.videos[2 * currentVideoIndex + 2].text;


            videoPlayer.Play();



        }
    }

    public void setIsInFinal(bool isInFinal)
    {
        is_in_final = isInFinal;
    }

    public void setIsInFeedback(bool isInFeedback)
    {
        is_in_feedback = isInFeedback;
    }

    public void PlayVideo(int buttonIndex)
    {
        Debug.Log("Button index:");
        Debug.Log(buttonIndex);
        Debug.Log(buttonIndex >= 0);
        Debug.Log(videoList.videos.Length);

        // Define o que fazer quando um botão é apertado, a depender dos estados do jogo
        if (is_in_final)
        {
            if (buttonIndex == 0 || is_in_feedback)
            {
                currentVideoIndex = 0;
                videoPlayer.url = videoList.videos[currentVideoIndex].url;
                buttonTexts[0].text = videoList.videos[2 * currentVideoIndex + 1].text;
                buttonTexts[1].text = videoList.videos[2 * currentVideoIndex + 2].text;
                videoPlayer.Play();
                is_in_final = false;
            }
            else if (!is_in_feedback)
            {
                is_in_feedback = true;
                is_in_final = false;
                currentVideoIndex = 0;
                videoPlayer.url = videoList.feedbackVideos[currentVideoIndex].url;
                buttonTexts[0].text = videoList.feedbackVideos[currentVideoIndex].option1;
                buttonTexts[1].text = videoList.feedbackVideos[currentVideoIndex].option2;
                videoPlayer.Play();

            }
        }
        else if (is_in_feedback)
        {
            currentVideoIndex++;
            videoPlayer.url = videoList.feedbackVideos[currentVideoIndex].url;
            
            if (videoList.feedbackVideos[currentVideoIndex].is_final)
            {
                is_in_final = true;
                //buttonControllers[0].enabled = false;
                //buttonTexts[0].text = "Voltar ao início";
                //buttonTexts[1].text = "Voltar ao início";
                buttons_canvas.SetActive(false);
            }
            else
            {
                buttonTexts[0].text = videoList.feedbackVideos[currentVideoIndex].option1;
                buttonTexts[1].text = videoList.feedbackVideos[currentVideoIndex].option2;
            }
            videoPlayer.Play();
        }
        else if (buttonIndex >= 0 && buttonIndex < videoList.videos.Length)
        {

            currentVideoIndex = 2*currentVideoIndex + buttonIndex + 1;
            videoPlayer.url = videoList.videos[currentVideoIndex].url;

            if (videoList.videos[currentVideoIndex].is_final) {
                videoPlayer.url = videoList.videos[currentVideoIndex].url;
                buttonTexts[0].text = "Voltar";
                buttonTexts[1].text = "Finalizar";
                is_in_final = true;
            }
            else
            {
                Debug.Log(currentVideoIndex);
                buttonTexts[0].text = videoList.videos[2 * currentVideoIndex + 1].text;
                buttonTexts[1].text = videoList.videos[2 * currentVideoIndex + 2].text;
            }

            videoPlayer.Play();
        }
    }
}



