using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image), typeof (Animator))]
public class UncutZone : MonoBehaviour
{
    [SerializeField] SpriteContainer _spriteContainer = null;
    [SerializeField] bool _hasFood;
    [SerializeField] Food _food;

    [SerializeField] int _numCutsSalad = 3;
    [SerializeField] int _numCutsOnion = 6;
    [SerializeField] int _numCutsTomato = 4;

    int _numCutsDone = 0;

    Image _image;
    Animator _animator;
    CutZone _cutZone;



    void Awake()
    {
        _image = GetComponent<Image>();
        _animator = GetComponent<Animator>();
        _cutZone = transform.parent.GetChild(1).GetComponent<CutZone>();
    }

    private void Start()
    {
        UpdateSprite();
    }

    public bool AddFood(Food food)
    {
        if (!_hasFood)
        {
            _hasFood = true;
            _food = food;
            UpdateSprite();
            return true;
        }
        else
        {
            _animator.SetTrigger("Wrong");
            return false;
        }
    }

    public void CutFoodChecker()
    {
        if (_hasFood)
        {
            _numCutsDone += 1;
            _animator.SetTrigger("Cut");
            if (_food == Food.Salad)
            {
                if (_numCutsDone >= _numCutsSalad)
                {
                    MoveToCutZone();
                }
            }
            else if (_food == Food.Tomato)
            {
                if (_numCutsDone >= _numCutsTomato)
                {
                    MoveToCutZone();
                }
            }
            else if(_food == Food.Onion)
            {
                if (_numCutsDone >= _numCutsOnion)
                {
                    MoveToCutZone();
                }
            }
        }
        else
        {
            _animator.SetTrigger("Wrong");
            Debug.Log("No food to cut");
        }
    }

    private void MoveToCutZone()
    {
        if (_cutZone.PlaceCutFood(_food))
        {
            Debug.Log("Food successfully cut.");
            _numCutsDone = 0;
            RemoveFood();
        }
    }

    private void RemoveFood()
    {
        _hasFood = false;
        UpdateSprite();
    }


    void UpdateSprite()
    {
        if (_hasFood)
        {
            switch (_food)
            {
                case Food.Salad:
                    _image.sprite = _spriteContainer.Salad;
                    break;
                case Food.Onion:
                    _image.sprite = _spriteContainer.Onion;
                    break;
                case Food.Tomato:
                    _image.sprite = _spriteContainer.Tomato;
                    break;
                default:
                    _image.sprite = null;
                    break;
            }
        }
        else
        {
            _image.sprite = _spriteContainer.Area;
            _image.color = Color.white;
        }
    }

}
