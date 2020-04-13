using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FridgeSlot : MonoBehaviour
{
    #region Private Fields
    [SerializeField] Food _rawFoodType = new Food();
    [SerializeField] int _amount = 100;

    Image[] _images = new Image[3];
    Color _initialIconColor;
    Color _transparentIconColor;

    #endregion

    #region Properties
    public int Amount { get => _amount; }
    public Food RawFoodType { get => _rawFoodType; }
    #endregion

    #region Public Functions
    public void AddAmount(int value)
    {
        _amount += value;
        CheckAmount();
    }

    public bool DecAmount()
    {
        if (_amount <= 0)
        {
            Debug.LogError("Could not deduct. Amount is 0");
            return false;
        }
        else
        {
            _amount -= 1;
            CheckAmount();
            return true;
        }
    }
    #endregion

    #region Private Functions
    private void CheckAmount()
    {
        if (_amount > 2)
        {
            foreach (Image image in _images)
            {
                image.color = _initialIconColor;
            }
        }
        else
        {
            for (int i = 0; i <= _amount; i++)
            {
                _images[i].color = _initialIconColor;
            }
            for (int i = _amount; i < _images.Length; i++)
            {
                _images[i].color = _transparentIconColor;
            }
        }
    }
    #endregion


    #region UnityFunctions
    private void Awake()
    {
        GetImageComponentFromChildren();

    }
    #endregion

    void GetImageComponentFromChildren()
    {
        int counter = 0;
        foreach (Transform child in transform)
        {
            _images[counter] = child.GetComponent<Image>();
            counter += 1;
        }
        SetupImageColors();
    }

    void SetupImageColors()
    {
        _initialIconColor = _images[0].color;
        _transparentIconColor = _initialIconColor;
        _transparentIconColor.a = 0.25f;
    }
}
