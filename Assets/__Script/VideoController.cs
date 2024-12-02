using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Video Player ����
    public Image playImage;          // Play �̹���
    public Image pauseImage;         // Pause �̹���
    public AudioSource audioSource;  // Audio Source ����
    private Coroutine hideCoroutine; // �ڷ�ƾ ���� ����

    void Start()
    {
        // ������ �ð��� 0���� �����Ͽ� ó������ �����ϵ��� ��
        videoPlayer.time = 0;

        // �⺻ �̹��� ���� ����
        playImage.gameObject.SetActive(true);
        pauseImage.gameObject.SetActive(false);

        // VideoPlayer ����: ����� ����� AudioSource�� ����
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // ���� ������ �� ó��
        videoPlayer.loopPointReached += HandleVideoEnd;
    }

    // ������ ��� / �Ͻ�����
    public void PlayVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playImage.gameObject.SetActive(false);  // Play ��ư �̹��� Ȱ��ȭ
            pauseImage.gameObject.SetActive(true);  // Pause ��ư �̹��� ��Ȱ��ȭ

            // ���� �ڷ�ƾ ���� (�ߺ� ����)
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }
        }
        else
        {
            videoPlayer.Play();
            playImage.gameObject.SetActive(true);  // Play ��ư �̹��� Ȱ��ȭ
            pauseImage.gameObject.SetActive(false);  // Pause ��ư �̹��� ��Ȱ��ȭ

            // ���� �ڷ�ƾ ���� �� �� �ڷ�ƾ ����
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
        yield return new WaitForSeconds(1f);  // 1�� ���
        playImage.gameObject.SetActive(false);  // playImage ��Ȱ��ȭ
    }

    private void HandleVideoEnd(VideoPlayer vp)
    {
        videoPlayer.time = 0;  // ������ ������ ó������ �ٽ� ����
        videoPlayer.Pause();    // �ٽ� ���
    }
}
