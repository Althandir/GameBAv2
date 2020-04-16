using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Language;
using System;

public class LanguageManager : MonoBehaviour
{
    static LanguageManager _instance;
    
    [SerializeField] Lang _usedLanguage = new Lang();

    Container _container;

    UnityEvent OnLanguageChanged = new UnityEvent();

    public static LanguageManager Instance { get => _instance;}
    public UnityEvent OnLanguageChangedEvent { get => OnLanguageChanged;}

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Multiple LanguageManager found.");
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        ChangeLanguage(Lang.en);
    }

    void ChangeLanguage(Lang newLanguage)
    {
        string countryTag;

        switch (newLanguage)
        {
            case Lang.en:
                countryTag = "en";
                break;
            case Lang.de:
                countryTag = "de";
                break;
            default:
                countryTag = "en";
                break;
        }
        _container = JsonUtility.FromJson<Container>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/language_" + countryTag +".json"));
        _usedLanguage = newLanguage;
        OnLanguageChanged.Invoke();
    }

    #region Getter methods for the container    
    public string GetTutorialButtonText()
    {
        return _container.TutorialButtonText;
    }

    public string GetSettingsOrderText()
    {
        return _container.SettingsOrderText;
    }

    public string GetSettingsRatDelayText()
    {
        return _container.SettingsRatDelayText;
    }

    public string GetSettingsButtonText()
    {
        return _container.SettingsButtonText;
    }

    public string GetExitButtonText()
    {
        return _container.ExitButtonText;
    }

    public string GetGameTitle()
    {
        return _container.MainMenuTitle;
    }

    public string GetGameSubtitle()
    {
        return _container.MainMenuSubtitle;
    }

    public string GetPlayButtonText()
    {
        return _container.PlayButtonText;
    }

    public string GetEndOfGameText()
    {
        return _container.EndOfGameText;
    }

    public string GetRatingText(int rating)
    {
        switch (rating)
        {
            case 0:
                return _container.zeroStarText[UnityEngine.Random.Range(0, _container.zeroStarText.Count - 1)];
            case 1:
                return _container.oneStarText[UnityEngine.Random.Range(0, _container.oneStarText.Count - 1)];
            case 2:
                return _container.twoStarText[UnityEngine.Random.Range(0, _container.twoStarText.Count - 1)];
            case 3:
                return _container.threeStarText[UnityEngine.Random.Range(0, _container.threeStarText.Count - 1)];
            case 4:
                return _container.fourStarText[UnityEngine.Random.Range(0, _container.fourStarText.Count - 1)];
            case 5:
                return _container.fiveStarText[UnityEngine.Random.Range(0, _container.fiveStarText.Count - 1)];
            default:
                return string.Empty;
        }
    }

    #endregion
}


