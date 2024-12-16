using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundAudio; // 배경음 오디오 소스
    public AudioSource[] effectAudios; // 효과음 오디오 소스 배열

    [Header("Sliders")]
    public Slider backgroundVolumeSlider; // 배경음 슬라이더
    public Slider effectVolumeSlider; // 효과음 슬라이더

    [Header("Volume Settings")]
    public float minVolume = 0f; // 최소 볼륨
    public float maxVolume = 1f; // 최대 볼륨

    void Start()
    {
        // 슬라이더 초기화
        if (backgroundVolumeSlider != null && backgroundAudio != null)
        {
            // 배경음 초기 볼륨을 슬라이더 값에 맞춤
            backgroundVolumeSlider.value = backgroundAudio.volume;
            backgroundVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        }

        if (effectVolumeSlider != null && effectAudios != null)
        {
            // 효과음 초기 볼륨을 슬라이더 값에 맞춤
            effectVolumeSlider.value = effectAudios.Length > 0 ? effectAudios[0].volume : 0f;
            effectVolumeSlider.onValueChanged.AddListener(SetEffectVolume);
        }

        // 배경음 재생
        if (backgroundAudio != null)
        {
            backgroundAudio.loop = true;
            backgroundAudio.Play();
        }
    }

    // 슬라이더 값 변경 시 호출되는 메서드
    public void SetBackgroundVolume(float value)
    {
        if (backgroundAudio != null)
        {
            backgroundAudio.volume = Mathf.Lerp(minVolume, maxVolume, value);
        }
    }

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
}
