using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    private Dictionary<string, string> localizedTexts;
    public string currentLanguage = "English";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadLocalizedText(currentLanguage);
    }

    // �� �ؽ�Ʈ �ε�
    public void LoadLocalizedText(string language)
    {
        localizedTexts = new Dictionary<string, string>();

        if (language == "English")
        {
            localizedTexts.Add("Play", "Play");
            localizedTexts.Add("Pause", "Pause");
        }
        else if (language == "Korean")
        {
            localizedTexts.Add("Play", "���");
            localizedTexts.Add("Pause", "�Ͻ�����");
        }
    }

    public string GetLocalizedText(string key)
    {
        return localizedTexts.ContainsKey(key) ? localizedTexts[key] : key;
    }

    public void ChangeLanguage(string newLanguage)
    {
        currentLanguage = newLanguage;
        LoadLocalizedText(newLanguage);
    }
}
