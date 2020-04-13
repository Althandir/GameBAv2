using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

[RequireComponent(typeof(Button), typeof(Animator))]
public class MarketZone : MonoBehaviour
{
    Button _button;
    Animator _animator;

    [SerializeField] GameObject[] _boxImages = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(OnClickHandler);
        MarketSystem.Instance.OrderFoodEvent.AddListener(OnOrderHandler);
    }

    void OnOrderHandler()
    {
        _animator.SetBool("OrderActive", MarketSystem.Instance.ActiveOrder);
        if (MarketSystem.Instance.ActiveOrder)
        {
            DisplayBoxes(true);
        }
        else
        {
            DisplayBoxes(false);
        }
    }

    void DisplayBoxes(bool status)
    {
        foreach (var box in _boxImages)
        {
            box.SetActive(status);
        }
    }


    void OnClickHandler()
    {
        if (MouseSystem.Instance.IsHolding || MouseSystem.Instance.IsHoldingKnife)
        {
            Debug.LogError("You are holding something.");
        }
        else if (!MouseSystem.Instance.enabled)
        {
            Debug.LogError("MouseSystem is not active.");
        }
        else if (MarketSystem.Instance.MarketUIEnabled)
        {
            Debug.Log("Market already open.");
        }
        else if (MarketSystem.Instance.ActiveOrder)
        {
            Debug.Log("An order is currently active.");
        }
        else
        {
            MarketSystem.Instance.OpenMarket();
            MouseSystem.Instance.enabled = false;
        }

    }


}
