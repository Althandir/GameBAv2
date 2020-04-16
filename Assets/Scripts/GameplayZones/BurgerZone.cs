using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerZone : MonoBehaviour
{
    static BurgerZone _instance;

    [SerializeField] Color _transparentColor = Color.white;

    bool _hasBuns;
    bool _hasMeat;
    bool _hasCheese;
    bool _hasSalad;
    bool _hasOnion;
    bool _hasTomato;

    Image[] _images;

    public bool HasBuns { get => _hasBuns; }
    public bool HasMeat { get => _hasMeat; }
    public bool HasCheese { get => _hasCheese; }
    public bool HasSalad { get => _hasSalad; }
    public bool HasOnion { get => _hasOnion; }
    public bool HasTomato { get => _hasTomato; }
    public static BurgerZone Instance { get => _instance; }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            _images = new Image[transform.childCount];
        }
        else
        {
            Debug.LogError("Multiple BurgerZones detected.");
        }
    }

    private void Start()
    {
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i] = transform.GetChild(i).GetComponent<Image>();
        }

        BellZone.Instance.BellPressedEvent.AddListener(OnBellPressed);

        ResetZone();
    }

    public void AddBuns()
    {
        _hasBuns = true;
        _images[0].color = Color.white;
        _images[6].color = Color.white;
    }

    public void AddMeat()
    {
        _hasMeat = true;
        _images[1].color = Color.white;
    }

    public void AddCheese()
    {
        _hasCheese = true;
        _images[2].color = Color.white;
    }

    public void AddCutSalad()
    {
        _hasSalad = true;
        _images[3].color = Color.white;
    }

    public void AddCutOnion()
    {
        _hasOnion = true;
        _images[5].color = Color.white;
    }

    public void AddCutTomato()
    {
        _hasTomato = true;
        _images[4].color = Color.white;
    }

    void OnBellPressed()
    {
        //yield return new WaitForSeconds(0.5f);
        ResetZone();
    }

    public void ResetZone()
    {
        _hasBuns = false;
        _hasCheese = false;
        _hasMeat = false;
        _hasOnion = false;
        _hasSalad = false;
        _hasTomato = false;

        foreach (Image image in _images)
        {
            image.color = _transparentColor;
        }
    }

    private void OnValidate()
    {
        _images = new Image[transform.childCount];
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i] = transform.GetChild(i).GetComponent<Image>();
            _images[i].color = _transparentColor;
        }
    }
}
