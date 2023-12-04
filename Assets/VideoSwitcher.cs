//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Video;

//public class VideoSwitcher : MonoBehaviour
//{
//    public List<VideoClip> videos;
//    public VideoPlayer videoPlayer;
//    private int currentVideoIndex = 0;

//    private void Start()
//    {
//        videoPlayer.clip = videos[currentVideoIndex];
//    }

//    public void PlayVideo(int buttonIndex)
//    {
//        if (buttonIndex >= 0 && buttonIndex < videos.Count)
//        {
//            currentVideoIndex = buttonIndex;
//            videoPlayer.clip = videos[currentVideoIndex];
//            videoPlayer.Play();
//        }
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

[System.Serializable]
public class VideoData
{
    public string url;
    public string text;
    public bool is_final;
}

[System.Serializable]
public class VideoList
{
    public VideoData[] videos;
}

public class VideoSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    //public TextAsset jsonFile;
    public List<TextMeshProUGUI> buttonTexts;
    private VideoList videoList;
    private int currentVideoIndex = 0;
    private bool is_in_final = false;

    private void Start()
    {
        Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
        TextAsset jsonFile = Resources.Load<TextAsset>("mock");
        Debug.Log(jsonFile);
        string jsonString = jsonFile.text;
        
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
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

    public void PlayVideo(int buttonIndex)
    {   
        if (is_in_final)
        {
            if (buttonIndex == 0)
            {
                currentVideoIndex = 0;
                videoPlayer.url = videoList.videos[currentVideoIndex].url;
                buttonTexts[0].text = videoList.videos[2 * currentVideoIndex + 1].text;
                buttonTexts[1].text = videoList.videos[2 * currentVideoIndex + 2].text;
                videoPlayer.Play();
                is_in_final = false;
            }
            else
            {
                is_in_final = false;
            }
        }
        else if (buttonIndex >= 0 && buttonIndex < videoList.videos.Length)
        {   
            Debug.Log(buttonIndex);
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


//public class VideoSwitcher : MonoBehaviour
//{
//    public List<string> videoUrls; // Lista de URLs dos vídeos
//    public VideoPlayer videoPlayer;
//    private int currentVideoIndex = 0;

//    private void Start()
//    {
//        TextAsset jsonFile = Resources.Load<TextAsset>("videos");
//        string jsonString = jsonFile.text;
//        videoList = JsonUtility.FromJson<VideoList>(jsonString);
//        Debug.Log("AAAAAAAAAAAAAA");
//        StartCoroutine(PlayFromURL(videoUrls[currentVideoIndex]));
//        Debug.Log("AAAAAAAAAAAAAA");
//    }

//    public void PlayVideo(int buttonIndex)
//    {
//        if (buttonIndex >= 0 && buttonIndex < videoUrls.Count)
//        {
//            currentVideoIndex = buttonIndex;
//            StartCoroutine(PlayFromURL(videoUrls[currentVideoIndex]));
//        }
//    }

//    IEnumerator PlayFromURL(string url)
//    {
//        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
//        {
//            yield return webRequest.SendWebRequest();
//            if (webRequest.isNetworkError || webRequest.isHttpError)
//            {
//                Debug.LogError(webRequest.error);
//            }
//            else
//            {
//                videoPlayer.url = webRequest.url;
//                videoPlayer.Play();
//            }
//        }
//    }
//}

