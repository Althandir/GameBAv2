using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CutZone : MonoBehaviour
{
    bool _isHolding;
    
    Food _cuttedFood;

    Image _image;

    [SerializeField] SpriteContainer _spriteContainer = null;

    public bool IsHolding { get => _isHolding;}
    public Food GetFood 
    {
        get 
        {
            _isHolding = false;
            UpdateFoodSprite();
            return _cuttedFood; 
        } 
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public bool PlaceCutFood(Food cuttedFood)
    {
        if (!_isHolding)
        {
            switch (cuttedFood)
            {
                case Food.Salad:
                    _cuttedFood = Food.cutSalad;
                    break;
                case Food.Onion:
                    _cuttedFood = Food.cutOnion;
                    break;
                case Food.Tomato:
                    _cuttedFood = Food.cutTomato;
                    break;
                default:
                    return false;
            }

            _isHolding = true;
            UpdateFoodSprite();
            return true;
        }
        else
        {
            Debug.Log("CutZone is full.");
            return false;
        }
    }

    void UpdateFoodSprite()
    {
        if (_isHolding)
        {
            _image.color = Color.white;
            switch (_cuttedFood)
            {
                case Food.cutSalad:
                    _image.sprite = _spriteContainer.CutSalad;
                    break;
                case Food.cutOnion:
                    _image.sprite = _spriteContainer.CutOnion;
                    break;
                case Food.cutTomato:
                    _image.sprite = _spriteContainer.CutTomato;
                    break;
                default:
                    _image.sprite = null;
                    break;
            }
        }
        else
        {
            _image.sprite = _spriteContainer.Area;
            _image.color = Color.clear;
        }
    }
}
