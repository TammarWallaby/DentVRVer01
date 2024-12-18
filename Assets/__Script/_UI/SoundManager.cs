using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundAudio; // ����� ����� �ҽ�
    public AudioSource[] effectAudios;  // ȿ���� ����� �ҽ� �迭

    [Header("Sliders")]
    public Slider backgroundVolumeSlider; // ����� �����̴�
    public Slider effectVolumeSlider;     // ȿ���� �����̴�

    [Header("Volume Settings")]
    public float minVolume = 0f; // �ּ� ����
    public float maxVolume = 1f; // �ִ� ����

    [Header("Background Clips")]
    public AudioClip backgroundClip1; // 1�� �����
    public AudioClip backgroundClip2; // 2�� �����

    private bool isPlayingClip1 = true; // ���� ��� ���� ������� ����

    void Start()
    {
        // ����� �����̴� �ʱ�ȭ
        if (backgroundVolumeSlider != null && backgroundAudio != null)
        {
            backgroundVolumeSlider.value = Mathf.InverseLerp(minVolume, maxVolume, backgroundAudio.volume);
            backgroundVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        }

        // ȿ���� �����̴� �ʱ�ȭ
        if (effectVolumeSlider != null && effectAudios != null && effectAudios.Length > 0)
        {
            effectVolumeSlider.value = Mathf.InverseLerp(minVolume, maxVolume, effectAudios[0].volume);
            effectVolumeSlider.onValueChanged.AddListener(SetEffectVolume);
        }

        // ����� �ʱ� ���� �� ���
        if (backgroundAudio != null)
        {
            backgroundAudio.clip = backgroundClip1; // �ʱ� ����� ����
            backgroundAudio.loop = true;
            backgroundAudio.Play();
        }
    }

    // ����� ���� ����
    public void SetBackgroundVolume(float value)
    {
        if (backgroundAudio != null)
        {
            backgroundAudio.volume = Mathf.Lerp(minVolume, maxVolume, value);
        }
    }

    // ȿ���� ���� ����
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

    // ��ư Ŭ�� �� ������� ��ȯ
    public void ChangeBackgroundMusic()
    {
        if (backgroundAudio == null) return;

        // ����� ��ȯ
        if (isPlayingClip1)
        {
            backgroundAudio.clip = backgroundClip2;
        }
        else
        {
            backgroundAudio.clip = backgroundClip1;
        }

        // ����� ��� �� ���� ��ȯ
        backgroundAudio.Play();
        isPlayingClip1 = !isPlayingClip1;
    }
}
