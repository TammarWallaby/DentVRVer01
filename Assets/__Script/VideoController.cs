using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Video Player 참조
    public Button playButton;        // 재생 버튼

    void Start()
    {
        // 버튼에 이벤트 연결
        playButton.onClick.AddListener(PlayVideo);
    }

    // 동영상 재생
    void PlayVideo()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }

        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }


}
