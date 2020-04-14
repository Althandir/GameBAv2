using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarketSystem : MonoBehaviour
{
    static MarketSystem _instance;
    public static MarketSystem Instance { get => _instance; }

    [SerializeField] MarketUIBody _marketBody = null;
    [SerializeField] GameObject _marketUI = null;
    [SerializeField] FridgeSlot[] _fridges = null;
    
    bool _activeOrder;
    IEnumerator _deliverFood;
    int _deliveryDelay;

    UnityEvent _orderFoodEvent = new UnityEvent();

    [SerializeField][Range(4,12)] int _maxDelay = 4;
    public bool MarketUIEnabled { get => _marketUI.activeSelf; }
    public bool ActiveOrder { get => _activeOrder; }
    public UnityEvent OrderFoodEvent { get => _orderFoodEvent; }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        _deliverFood = DeliverFood();
    }

    public void OpenMarket()
    {
        _marketUI.SetActive(true);
    }

    public void CloseMarket()
    {
        _marketUI.SetActive(false);
    }

    public void OrderFood()
    {
        if (_activeOrder)
        {
            Debug.LogError("Can't order food. An order is already running.");
        }
        else
        {
            StartCoroutine(DeliverFood());
        }
    }

    IEnumerator DeliverFood()
    {
        _activeOrder = true;
        _orderFoodEvent.Invoke();
        while (_deliveryDelay < _maxDelay)
        {
            yield return new WaitForSeconds(1);
            _deliveryDelay += 1;
        }
        _deliveryDelay = 0;

        AddFoodToFridges();
        _marketBody.ResetBody();

        _activeOrder = false;
        _orderFoodEvent.Invoke();
    }

    private void AddFoodToFridges()
    {
        foreach (var UIAmount in _marketBody.Amounts)
        {
            switch (UIAmount.Type)
            {
                case Food.Bun:
                    _fridges[0].AddAmount(UIAmount.Amount);
                    break;
                case Food.rawMeat:
                    _fridges[1].AddAmount(UIAmount.Amount);
                    break;
                case Food.Cheese:
                    _fridges[2].AddAmount(UIAmount.Amount);
                    break;
                case Food.Salad:
                    _fridges[3].AddAmount(UIAmount.Amount);
                    break;
                case Food.Onion:
                    _fridges[4].AddAmount(UIAmount.Amount);
                    break;
                case Food.Tomato:
                    _fridges[5].AddAmount(UIAmount.Amount);
                    break;
                default:
                    break;
            }
            Debug.Log("Delivered " + UIAmount.Amount + " of " + UIAmount.Type + " into the fridge.");
        }
    }
}
