using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Video Player 참조
    public Button playButton;        // 재생 버튼
    public Image playImage;
    public Image pauseImage;



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
            playImage.gameObject.SetActive(true);
            pauseImage.gameObject.SetActive(false);
            StartCoroutine(HidePlayImageAfterDelay());
        }

        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playImage.gameObject.SetActive(false);
            pauseImage.gameObject.SetActive(true);
        }

    }
    private IEnumerator HidePlayImageAfterDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기
        playImage.gameObject.SetActive(false);  // playImage 비활성화
    }
}

