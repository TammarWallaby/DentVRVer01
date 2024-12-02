using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Video Player ����
    public Button playButton;        // ��� ��ư
    public Image playImage;
    public Image pauseImage;



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
        yield return new WaitForSeconds(1f);  // 1�� ���
        playImage.gameObject.SetActive(false);  // playImage ��Ȱ��ȭ
    }
}

