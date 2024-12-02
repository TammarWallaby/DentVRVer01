using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Video Player ����
    public Button playButton;        // ��� ��ư

    void Start()
    {
        // ��ư�� �̺�Ʈ ����
        playButton.onClick.AddListener(PlayVideo);
    }

    // ������ ���
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
