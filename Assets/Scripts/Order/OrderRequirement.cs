using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderRequirement : MonoBehaviour
{
    Order _order;
    bool _isRequested;

    [SerializeField] Food _requestedFood = new Food();
    GameObject _sprites = null;
    Image _statusImage;

    private void Awake()
    {
        _order = transform.parent.GetComponent<Order>();
        _sprites = transform.GetChild(0).gameObject;
        _statusImage = _sprites.transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        if (!_order)
        {
            Debug.Log("Could not find Order. " + gameObject.name);
        }
        else
        {
            _order.ActivateEvent.AddListener(OnActivate);
            _order.DeactivateEvent.AddListener(OnDeactivate);
        }

        OnDeactivate();
    }

    void OnDeactivate()
    {
        _sprites.SetActive(false);
    }


    void OnActivate()
    {
        _sprites.SetActive(true);

        switch (_requestedFood)
        {
            case Food.Cheese:
                _isRequested = _order.WantCheese;
                break;
            case Food.Salad:
                _isRequested = _order.WantSalad;
                break;
            case Food.Onion:
                _isRequested = _order.WantOnion;
                break;
            case Food.Tomato:
                _isRequested = _order.WantTomato;
                break;
            case Food.cookedMeat:
                _isRequested = _order.WantMeat;
                break;
            default:
                Debug.Log("Wrong Food requested in OrderRequirement.");
                break;
        }

        UpdateSprites();
    }

    void UpdateSprites()
    {
        if (_order.IsActive)
        {
            if (_isRequested)
            {
                _statusImage.sprite = _order.SpriteContainer.GreenCheck;
            }
            else
            {
                _statusImage.sprite = _order.SpriteContainer.RedCross;
            }
        }
        else
        {
            _statusImage.sprite = null;
        }
    }
}
