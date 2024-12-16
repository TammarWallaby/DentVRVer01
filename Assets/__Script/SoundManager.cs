using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundAudio; // ����� ����� �ҽ�
    public AudioSource[] effectAudios; // ȿ���� ����� �ҽ� �迭

    [Header("Sliders")]
    public Slider backgroundVolumeSlider; // ����� �����̴�
    public Slider effectVolumeSlider; // ȿ���� �����̴�

    [Header("Volume Settings")]
    public float minVolume = 0f; // �ּ� ����
    public float maxVolume = 1f; // �ִ� ����

    void Start()
    {
        // �����̴� �ʱ�ȭ
        if (backgroundVolumeSlider != null && backgroundAudio != null)
        {
            // ����� �ʱ� ������ �����̴� ���� ����
            backgroundVolumeSlider.value = backgroundAudio.volume;
            backgroundVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        }

        if (effectVolumeSlider != null && effectAudios != null)
        {
            // ȿ���� �ʱ� ������ �����̴� ���� ����
            effectVolumeSlider.value = effectAudios.Length > 0 ? effectAudios[0].volume : 0f;
            effectVolumeSlider.onValueChanged.AddListener(SetEffectVolume);
        }

        // ����� ���
        if (backgroundAudio != null)
        {
            backgroundAudio.loop = true;
            backgroundAudio.Play();
        }
    }

    // �����̴� �� ���� �� ȣ��Ǵ� �޼���
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
