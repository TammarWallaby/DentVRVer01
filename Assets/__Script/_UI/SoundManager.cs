using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundAudio; // 배경음 오디오 소스
    public AudioSource[] effectAudios;  // 효과음 오디오 소스 배열

    [Header("Sliders")]
    public Slider backgroundVolumeSlider; // 배경음 슬라이더
    public Slider effectVolumeSlider;     // 효과음 슬라이더

    [Header("Volume Settings")]
    public float minVolume = 0f; // 최소 볼륨
    public float maxVolume = 1f; // 최대 볼륨

    [Header("Background Clips")]
    public AudioClip backgroundClip1; // 1번 배경음
    public AudioClip backgroundClip2; // 2번 배경음

    private bool isPlayingClip1 = true; // 현재 재생 중인 배경음을 추적

    void Start()
    {
        // 배경음 슬라이더 초기화
        if (backgroundVolumeSlider != null && backgroundAudio != null)
        {
            backgroundVolumeSlider.value = Mathf.InverseLerp(minVolume, maxVolume, backgroundAudio.volume);
            backgroundVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        }

        // 효과음 슬라이더 초기화
        if (effectVolumeSlider != null && effectAudios != null && effectAudios.Length > 0)
        {
            effectVolumeSlider.value = Mathf.InverseLerp(minVolume, maxVolume, effectAudios[0].volume);
            effectVolumeSlider.onValueChanged.AddListener(SetEffectVolume);
        }

        // 배경음 초기 설정 및 재생
        if (backgroundAudio != null)
        {
            backgroundAudio.clip = backgroundClip1; // 초기 배경음 설정
            backgroundAudio.loop = true;
            backgroundAudio.Play();
        }
    }

    // 배경음 볼륨 설정
    public void SetBackgroundVolume(float value)
    {
        if (backgroundAudio != null)
        {
            backgroundAudio.volume = Mathf.Lerp(minVolume, maxVolume, value);
        }
    }

    // 효과음 볼륨 설정
    public void SetEffectVolume(float value)
    {
        if (effectAudios != null)
        {
            foreach (var effectAudio in effectAudios)
            {
                if (effectAudio != null)
                {
                    effectAudio.volume = Mathf.Lerp(minVolume, maxVolume, value);
                }
            }
        }
    }

    // 버튼 클릭 시 배경음을 전환
    public void ChangeBackgroundMusic()
    {
        if (backgroundAudio == null) return;

        // 배경음 전환
        if (isPlayingClip1)
        {
            backgroundAudio.clip = backgroundClip2;
        }
        else
        {
            backgroundAudio.clip = backgroundClip1;
        }

        // 배경음 재생 및 상태 전환
        backgroundAudio.Play();
        isPlayingClip1 = !isPlayingClip1;
    }
}
