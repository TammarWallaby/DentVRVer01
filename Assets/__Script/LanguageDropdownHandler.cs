using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdownHandler : MonoBehaviour
{
    public Dropdown languageDropdown;

    void Start()
    {
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        SetInitialLanguage();
    }

    void SetInitialLanguage()
    {
        string currentLang = LanguageManager.Instance.currentLanguage;
        if (currentLang == "English")
            languageDropdown.value = 0;
        else if (currentLang == "Korean")
            languageDropdown.value = 1;
    }

    void OnLanguageChanged(int index)
    {
        string selectedLanguage = languageDropdown.options[index].text;
        LanguageManager.Instance.ChangeLanguage(selectedLanguage);
        UpdateUITexts();
    }

    void UpdateUITexts()
    {
        Text[] texts = FindObjectsOfType<Text>();
        foreach (Text text in texts)
        {
            text.text = LanguageManager.Instance.GetLocalizedText(text.name);
        }
    }
}
