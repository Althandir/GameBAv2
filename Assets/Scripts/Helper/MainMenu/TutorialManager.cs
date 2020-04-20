using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu = null;

    [SerializeField] Sprite[] _spritesEN = null;
    [SerializeField] Sprite[] _spritesDE = null;
    
    Image _image = null;
    Sprite[] _usedSprites = null;

    int currentSpriteId = 0;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.preserveAspect = true;
    }

    private void OnEnable()
    {
        currentSpriteId = 0;

        switch (LanguageManager.Instance.UsedLanguage)
        {
            case Language.Lang.en:
                _usedSprites = _spritesEN;
                break;
            case Language.Lang.de:
                _usedSprites = _spritesDE;
                break;
            default:
                break;
        }

        DisplayTutorial();
    }

    private void DisplayTutorial()
    {
        _image.sprite = _usedSprites[0];
    }

    public void NextSprite()
    {
        if (currentSpriteId < _usedSprites.Length-1)
        {
            currentSpriteId += 1;
            _image.sprite = _usedSprites[currentSpriteId];
        }
        else
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _mainMenu.SetActive(true);
    }
}
