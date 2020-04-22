using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSettingsValueHelper : MonoBehaviour
{
    [SerializeField] Button _orderPlus = null;
    [SerializeField] Button _orderMinus = null;

    [SerializeField] Button _ratPlus = null;
    [SerializeField] Button _ratMinus = null;

    private void Start()
    {
        _orderPlus.onClick.AddListener(OnOrderPlus);
        _orderMinus.onClick.AddListener(OnOrderMinus);

        _ratPlus.onClick.AddListener(OnRatPlus);
        _ratMinus.onClick.AddListener(OnRatMinus);
    }

    private void OnRatMinus()
    {
        GameSettings.Instance.DecRatDelay();
    }

    private void OnRatPlus()
    {
        GameSettings.Instance.AddRatDelay();
    }

    private void OnOrderMinus()
    {
        GameSettings.Instance.DecNumMaxOrder();
    }

    private void OnOrderPlus()
    {
        GameSettings.Instance.AddNumMaxOrder();
    }
}
