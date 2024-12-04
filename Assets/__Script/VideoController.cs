using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;       // Video Player 참조
    public Image playImage;               // Play 이미지
    public Image pauseImage;              // Pause 이미지
    public AudioSource audioSource;       // Audio Source 참조
    public VideoClip[] videoClips;        // 비디오 클립 배열
    private int currentVideoIndex = 0;    // 현재 비디오 인덱스
    private Coroutine hideCoroutine;      // 코루틴 참조 변수

    void Start()
    {
        playImage.gameObject.SetActive(true);
        pauseImage.gameObject.SetActive(false);

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.loopPointReached += HandleVideoEnd;

        PlayCurrentVideo();
    }

    // 현재 비디오 재생
    private void PlayCurrentVideo()
    {
        if (videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];  // 현재 인덱스의 비디오 설정
            videoPlayer.time = 0;                              // 비디오 시간 초기화
            videoPlayer.Play();                                // 비디오 재생
            playImage.gameObject.SetActive(false);             // Play 이미지 비활성화
            pauseImage.gameObject.SetActive(true);             // Pause 이미지 활성화

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HidePauseImageAfterDelay());
        }
    }

    // 동영상 재생 / 일시정지
    public void PlayVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playImage.gameObject.SetActive(true);              // Play 이미지 활성화
            pauseImage.gameObject.SetActive(false);            // Pause 이미지 비활성화
        }
        else
        {
            videoPlayer.Play();
            playImage.gameObject.SetActive(false);             // Play 이미지 비활성화
            pauseImage.gameObject.SetActive(true);             // Pause 이미지 활성화

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HidePauseImageAfterDelay());
        }
    }

    // 1초 후 playImage 비활성화
    private IEnumerator HidePauseImageAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        playImage.gameObject.SetActive(false);
    }

    // 영상 끝났을 때 처리
    private void HandleVideoEnd(VideoPlayer vp)
    {
        videoPlayer.time = 0;                                  // 영상 끝나면 시간 초기화
        videoPlayer.Stop();                                    // 영상 멈춤
        playImage.gameObject.SetActive(true);                  // Play 이미지 활성화
        pauseImage.gameObject.SetActive(false);                // Pause 이미지 비활성화
    }

    // 다음 비디오 재생
    public void NextVideo()
    {
        videoPlayer.Stop();                                    // 현재 비디오 멈춤
        currentVideoIndex = (currentVideoIndex + 1) % videoClips.Length;  // 다음 인덱스로 이동
        PlayCurrentVideo();
    }

    // 이전 비디오 재생
    public void PreviousVideo()
    {
        videoPlayer.Stop();                                    // 현재 비디오 멈춤
        currentVideoIndex = (currentVideoIndex - 1 + videoClips.Length) % videoClips.Length;  // 이전 인덱스로 이동 (음수 방지)
        PlayCurrentVideo();
    }
}
