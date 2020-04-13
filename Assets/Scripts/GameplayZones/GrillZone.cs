using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrillZone : MonoBehaviour
{
    Image _image = null;
    Color _rawMeatColor;
    Color _goodMeatColor;
    Color _burnedMeatColor;

    bool _hasMeat;
    [SerializeField] float _grillTimer = 0.0f;
    [SerializeField] float _grillTimeGood = 5.0f;
    [SerializeField] float _grillTimeBurned = 10.0f;
    [Range(0.01f, 0.05f)]
    [SerializeField] float _colorGradingPower = 0.01f;

    private void Awake()
    {
        _image = transform.GetChild(0).GetComponent<Image>();
        _rawMeatColor = new Color(1, 0.4f, 1, 1);
        _goodMeatColor = Color.white;
        _burnedMeatColor = Color.gray;
    }

    public bool HasMeat { get => _hasMeat; }

    IEnumerator GrillRoutine()
    {
        float timeCounter = 0.1f;
        _image.enabled = true;
        while (_hasMeat)
        {
            yield return new WaitForSeconds(timeCounter);
            _grillTimer += timeCounter;
            ColorgradeMeat();
        }
        _image.enabled = false;
        yield return null;
    }

    void ColorgradeMeat()
    {
        if (_grillTimer < _grillTimeGood)
        {
            if (_image.color.g < 1)
            {
                _image.color += new Color(0, _colorGradingPower, 0, 0);
            }
        }
        else if (_grillTimer < _grillTimeBurned && _grillTimer > _grillTimeGood)
        {
            _image.color -= new Color(_colorGradingPower, _colorGradingPower, _colorGradingPower, 0);
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
            _image.color = _rawMeatColor;
        }
        else if (meat == Food.cookedMeat)
        {
            _grillTimer = _grillTimeGood;
            _image.color = _goodMeatColor;
        }
        else if (meat == Food.burnedMeat)
        {
            _grillTimer = _grillTimeBurned;
            _image.color = _burnedMeatColor;
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
