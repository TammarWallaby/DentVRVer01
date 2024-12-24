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

    [Header("Background Audio Sources")]
    public AudioSource backgroundSource1; // 1�� ����� ����� �ҽ�
    public AudioSource backgroundSource2; // 2�� ����� ����� �ҽ�

    private bool isPlayingSource1 = true; // ���� ��� ���� ����� ����� �ҽ��� ����

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
        if (backgroundSource1 != null)
        {
            backgroundSource1.loop = true;
            backgroundSource1.Play();
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

    // ����� ����� �ҽ� ��ȯ
    public void ChangeBackgroundMusic()
    {
        if (backgroundSource1 == null || backgroundSource2 == null) return;

        // ����� ����� �ҽ� ��ȯ
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

        // ����� ��� �� ���� ��ȯ
        isPlayingSource1 = !isPlayingSource1;
    }
}
