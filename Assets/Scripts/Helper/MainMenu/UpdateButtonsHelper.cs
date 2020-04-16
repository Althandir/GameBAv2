using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateButtonsHelper : MonoBehaviour
{
    [SerializeField] TMP_Text _playText = null;
    [SerializeField] TMP_Text _tutorialText = null;
    [SerializeField] TMP_Text _settingsText = null;
    [SerializeField] TMP_Text _exitText = null;

    void Awake()
    {
        LanguageManager.Instance.OnLanguageChangedEvent.AddListener(OnLanguageChanged);
    }

    private void OnLanguageChanged()
    {
        _playText.text = LanguageManager.Instance.GetPlayButtonText();
        _tutorialText.text = LanguageManager.Instance.GetTutorialButtonText();
        _settingsText.text = LanguageManager.Instance.GetSettingsButtonText();
        _exitText.text = LanguageManager.Instance.GetExitButtonText();
    }

}
