using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Video Player 참조
    public Image playImage;          // Play 이미지
    public Image pauseImage;         // Pause 이미지
    public AudioSource audioSource;  // Audio Source 참조
    private Coroutine hideCoroutine; // 코루틴 참조 변수

    void Start()
    {

        // 기본 이미지 상태 설정
        playImage.gameObject.SetActive(true);
        pauseImage.gameObject.SetActive(false);

        // VideoPlayer 설정: 오디오 출력을 AudioSource로 설정
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
    }

    // 동영상 재생 / 일시정지
    public void PlayVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playImage.gameObject.SetActive(false);  // Play 버튼 이미지 활성화
            pauseImage.gameObject.SetActive(true); // Pause 버튼 이미지 비활성화

            // 기존 코루틴 중지 (중복 방지)
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }
        }
        else
        {
            videoPlayer.Play();
            playImage.gameObject.SetActive(true);  // Play 버튼 이미지 활성화
            pauseImage.gameObject.SetActive(false);  // Pause 버튼 이미지 활성화

            // 기존 코루틴 중지 후 새 코루틴 시작
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
        yield return new WaitForSeconds(1f);  // 1초 대기
        playImage.gameObject.SetActive(false);  // playImage 비활성화
    }
}
