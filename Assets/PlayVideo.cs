using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public int videoIndex;
    public VideoSwitcher videoSwitcher;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlayVideo);
    }

    public void PlayVideo()
    {
        Debug.Log(videoSwitcher);
        Debug.Log(videoIndex);
        videoSwitcher.PlayVideo(videoIndex);
    }
}
