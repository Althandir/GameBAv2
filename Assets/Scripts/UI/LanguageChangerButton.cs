using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChangerButton : MonoBehaviour
{
    [SerializeField]
    Language.Lang _targetLanguage = new Language.Lang();
    Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(ChangeLanguage);
    }

    void ChangeLanguage()
    {
        LanguageManager.Instance.ChangeLanguage(_targetLanguage);
    }
}

