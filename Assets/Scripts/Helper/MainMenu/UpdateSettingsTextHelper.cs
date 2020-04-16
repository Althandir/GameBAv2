using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateSettingsTextHelper : MonoBehaviour
{
    [SerializeField] TMP_Text orderText = null;
    [SerializeField] TMP_Text ratText = null;

    void Start()
    {
        LanguageManager.Instance.OnLanguageChangedEvent.AddListener(UpdateText);
    }

    private void UpdateText()
    {
        orderText.text = LanguageManager.Instance.GetSettingsOrderText();
        ratText.text = LanguageManager.Instance.GetSettingsRatDelayText();
    }
}
