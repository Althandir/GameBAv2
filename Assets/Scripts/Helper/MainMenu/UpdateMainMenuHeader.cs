using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateMainMenuHeader : MonoBehaviour
{
    [SerializeField] TMP_Text title = null;
    [SerializeField] TMP_Text subTitle = null;

    private void Awake()
    {
        LanguageManager.Instance.OnLanguageChangedEvent.AddListener(OnLanguageChanged);
    }

    private void OnLanguageChanged()
    {
        title.text = LanguageManager.Instance.GetGameTitle();
        subTitle.text = LanguageManager.Instance.GetGameSubtitle();
    }
}
