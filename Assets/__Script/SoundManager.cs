using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundAudio;
    public AudioSource[] effectAudios; // ���� ȿ������ ó���ϱ� ���� �迭

    [Header("Sliders")]
    public Transform backgroundVolumeSlider;
    public Transform effectVolumeSlider;

    [Header("Slider Settings")]
    public float sliderHeight = 0.2f; // �����̴��� �̵��� �� �ִ� ���� ����
    public float minVolume = 0f;
    public float maxVolume = 1f;

    private Vector3 backgroundSliderStartPos;
    private Vector3 effectSliderStartPos;

    void Start()
    {
        // �����̴��� �ʱ� ��ġ�� ����
        if (backgroundVolumeSlider != null)
            backgroundSliderStartPos = backgroundVolumeSlider.localPosition;

        if (effectVolumeSlider != null)
            effectSliderStartPos = effectVolumeSlider.localPosition;
    }

    void Update()
    {
        if (backgroundVolumeSlider != null && backgroundAudio != null)
        {
            // �����̴� ��ġ�� ������� ����� ���� ���
            float backgroundVolume = Mathf.Clamp01((backgroundVolumeSlider.localPosition.y - backgroundSliderStartPos.y) / sliderHeight);
            backgroundAudio.volume = Mathf.Lerp(minVolume, maxVolume, backgroundVolume);
        }

        if (effectVolumeSlider != null && effectAudios != null)
        {
            // �����̴� ��ġ�� ������� ȿ���� ���� ���
            float effectVolume = Mathf.Clamp01((effectVolumeSlider.localPosition.y - effectSliderStartPos.y) / sliderHeight);

            // �迭�� �ִ� ��� ȿ������ ���� ����
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

