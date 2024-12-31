/*
 * AudioManager 안에 넣음
 * 배경음 효과음 실린더로 설정
 * 배경음 눈송이 누르면 변경
 */
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

    [Header("Background Audio Sources")]
    public AudioSource backgroundSource1; // 1번 배경음 오디오 소스
    public AudioSource backgroundSource2; // 2번 배경음 오디오 소스

    private bool isPlayingSource1 = true; // 현재 재생 중인 배경음 오디오 소스를 추적

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
        if (backgroundSource1 != null)
        {
            backgroundSource1.loop = true;
            backgroundSource1.Play();
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

    // 배경음 오디오 소스 전환
    public void ChangeBackgroundMusic() //크리스마스 모드 on/off 일 때, 배경음 교체
    {
        if (backgroundSource1 == null || backgroundSource2 == null) return;

        // 배경음 오디오 소스 전환
        if (isPlayingSource1)
        {
            backgroundSource1.Stop();
            backgroundSource2.Play();
        }
        else
        {
            backgroundSource2.Stop();
            backgroundSource1.Play();
        }

        // 배경음 재생 및 상태 전환
        isPlayingSource1 = !isPlayingSource1;
    }
}
