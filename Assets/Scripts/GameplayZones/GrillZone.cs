using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GrillZone : MonoBehaviour
{
    AudioSource _audioSource = null;

    [SerializeField] AudioClip _rawMeatOnGrillAudio = null;
    [SerializeField] AudioClip _goodMeatOnGrillAudio = null;

    Image _meatSprite = null;
    Image _progressImage = null;
    Color _rawMeatColor;
    Color _goodMeatColor;
    Color _burnedMeatColor;

    bool _hasMeat;
    
    [SerializeField] float _grillTimer = 0.0f;
    [SerializeField] float _grillTimeGood = 10.0f;
    [SerializeField] float _grillTimeBurned = 20.0f;
    [Range(0.0025f, 0.05f)]
    [SerializeField] float _colorGradingPower = 0.025f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _meatSprite = transform.GetChild(0).GetComponent<Image>();
        _progressImage = transform.GetChild(1).GetComponent<Image>();
        _rawMeatColor = new Color(1, 0.4f, 1, 1);
        _goodMeatColor = Color.white;
        _burnedMeatColor = Color.gray;
    }

    public bool HasMeat { get => _hasMeat; }

    IEnumerator GrillRoutine()
    {
        float timeCounter = 0.025f;
        _meatSprite.enabled = true;
        _progressImage.enabled = true;
        while (_hasMeat)
        {
            yield return new WaitForSeconds(timeCounter);
            _grillTimer += timeCounter;
            ColorgradeMeat();
            UpdateProgessCircle();
            PlayAudio();
        }

        StopAudio();
        _meatSprite.enabled = false;
        _progressImage.enabled = false;
        _progressImage.fillAmount = 0.0f;
        yield return null;
    }

    void ColorgradeMeat()
    {
        if (_grillTimer < _grillTimeGood)
        {
            if (_meatSprite.color.g < 1)
            {
                _meatSprite.color += new Color(0, _colorGradingPower, 0, 0);
            }
        }
        else if (_grillTimer < _grillTimeBurned && _grillTimer > _grillTimeGood )
        {
            if (_meatSprite.color.r > 0.33f)
            {
                _meatSprite.color -= new Color(_colorGradingPower, _colorGradingPower, _colorGradingPower, 0);
            }
        }
    }

    void UpdateProgessCircle()
    {
        if (_grillTimer < _grillTimeGood)
        {
            _progressImage.color = Color.green;
            _progressImage.fillAmount = _grillTimer / _grillTimeGood;
        }
        else if (_grillTimer > _grillTimeGood && _grillTimer <= _grillTimeBurned )
        {
            _progressImage.color = Color.red;
            _progressImage.fillAmount = (_grillTimer - _grillTimeGood) / _grillTimeGood;
        }
    }

    private void PlayAudio()
    {
        if (!_audioSource.isPlaying)
        {
            if (_grillTimer < _grillTimeGood)
            {
                _audioSource.PlayOneShot(_rawMeatOnGrillAudio, 0.25f);
            }
            else 
            {
                _audioSource.PlayOneShot(_goodMeatOnGrillAudio, 0.25f);
            }
        }
    }

    void StopAudio()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    public bool AddMeat(Food meat)
    {
        if (!HasMeat)
        {
            _hasMeat = true;
            CheckMeat(meat);
            StartCoroutine(GrillRoutine());
            return true;
        }
        else
        {
            Debug.LogError("Grillplate already filled.");
            return false;
        }
    }

    void CheckMeat(Food meat)
    {
        if (meat == Food.rawMeat)
        {
            _grillTimer = 0.0f;
            _meatSprite.color = _rawMeatColor;
        }
        else if (meat == Food.cookedMeat)
        {
            _grillTimer = _grillTimeGood;
            _meatSprite.color = _goodMeatColor;
        }
        else if (meat == Food.burnedMeat)
        {
            _grillTimer = _grillTimeBurned;
            _meatSprite.color = _burnedMeatColor;
        }
    }

    public Food RemoveMeat()
    {
        _hasMeat = false;
        if (_grillTimer < _grillTimeGood)
        {
            return Food.rawMeat;
        }
        else if (_grillTimer > _grillTimeGood && _grillTimer < _grillTimeBurned)
        {
            return Food.cookedMeat;
        }
        else
        {
            return Food.burnedMeat;
        }
    }



}
