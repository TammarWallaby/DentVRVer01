using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundAudio;
    public AudioSource[] effectAudios; // 여러 효과음을 처리하기 위한 배열

    [Header("Sliders")]
    public Transform backgroundVolumeSlider;
    public Transform effectVolumeSlider;

    [Header("Slider Settings")]
    public float sliderHeight = 0.2f; // 슬라이더가 이동할 수 있는 높이 범위
    public float minVolume = 0f;
    public float maxVolume = 1f;

    private Vector3 backgroundSliderStartPos;
    private Vector3 effectSliderStartPos;

    void Start()
    {
        // 슬라이더의 초기 위치를 저장
        if (backgroundVolumeSlider != null)
            backgroundSliderStartPos = backgroundVolumeSlider.localPosition;

        if (effectVolumeSlider != null)
            effectSliderStartPos = effectVolumeSlider.localPosition;
    }

    void Update()
    {
        if (backgroundVolumeSlider != null && backgroundAudio != null)
        {
            // 슬라이더 위치를 기반으로 배경음 볼륨 계산
            float backgroundVolume = Mathf.Clamp01((backgroundVolumeSlider.localPosition.y - backgroundSliderStartPos.y) / sliderHeight);
            backgroundAudio.volume = Mathf.Lerp(minVolume, maxVolume, backgroundVolume);
        }

        if (effectVolumeSlider != null && effectAudios != null)
        {
            // 슬라이더 위치를 기반으로 효과음 볼륨 계산
            float effectVolume = Mathf.Clamp01((effectVolumeSlider.localPosition.y - effectSliderStartPos.y) / sliderHeight);

            // 배열에 있는 모든 효과음의 볼륨 적용
            foreach (var effectAudio in effectAudios)
            {
                if (effectAudio != null)
                {
                    effectAudio.volume = Mathf.Lerp(minVolume, maxVolume, effectVolume);
                }
            }
        }
    }
}

