using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;       // Video Player ����
    public Image playImage;               // Play �̹���
    public Image pauseImage;              // Pause �̹���
    public AudioSource audioSource;       // Audio Source ����
    public VideoClip[] videoClips;        // ���� Ŭ�� �迭
    private int currentVideoIndex = 0;    // ���� ���� �ε���
    private Coroutine hideCoroutine;      // �ڷ�ƾ ���� ����

    void Start()
    {
        playImage.gameObject.SetActive(true);
        pauseImage.gameObject.SetActive(false);

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.loopPointReached += HandleVideoEnd;

        PlayCurrentVideo();
    }

    // ���� ���� ���
    private void PlayCurrentVideo()
    {
        if (videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];  // ���� �ε����� ���� ����
            videoPlayer.time = 0;                              // ���� �ð� �ʱ�ȭ
            videoPlayer.Play();                                // ���� ���
            playImage.gameObject.SetActive(false);             // Play �̹��� ��Ȱ��ȭ
            pauseImage.gameObject.SetActive(true);             // Pause �̹��� Ȱ��ȭ

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HidePauseImageAfterDelay());
        }
    }

    // ������ ��� / �Ͻ�����
    public void PlayVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playImage.gameObject.SetActive(true);              // Play �̹��� Ȱ��ȭ
            pauseImage.gameObject.SetActive(false);            // Pause �̹��� ��Ȱ��ȭ
        }
        else
        {
            videoPlayer.Play();
            playImage.gameObject.SetActive(false);             // Play �̹��� ��Ȱ��ȭ
            pauseImage.gameObject.SetActive(true);             // Pause �̹��� Ȱ��ȭ

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HidePauseImageAfterDelay());
        }
    }

    // 1�� �� playImage ��Ȱ��ȭ
    private IEnumerator HidePauseImageAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        playImage.gameObject.SetActive(false);
    }

    // ���� ������ �� ó��
    private void HandleVideoEnd(VideoPlayer vp)
    {
        videoPlayer.time = 0;                                  // ���� ������ �ð� �ʱ�ȭ
        videoPlayer.Stop();                                    // ���� ����
        playImage.gameObject.SetActive(true);                  // Play �̹��� Ȱ��ȭ
        pauseImage.gameObject.SetActive(false);                // Pause �̹��� ��Ȱ��ȭ
    }

    // ���� ���� ���
    public void NextVideo()
    {
        videoPlayer.Stop();                                    // ���� ���� ����
        currentVideoIndex = (currentVideoIndex + 1) % videoClips.Length;  // ���� �ε����� �̵�
        PlayCurrentVideo();
    }

    // ���� ���� ���
    public void PreviousVideo()
    {
        videoPlayer.Stop();                                    // ���� ���� ����
        currentVideoIndex = (currentVideoIndex - 1 + videoClips.Length) % videoClips.Length;  // ���� �ε����� �̵� (���� ����)
        PlayCurrentVideo();
    }
}
