using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ConfirmOrderButton : MonoBehaviour
{
    Button _confirmButton;

    private void Awake()
    {
        _confirmButton = GetComponent<Button>();
    }

    private void Start()
    {
        _confirmButton.onClick.AddListener(OnClickHandler);
    }

    private void OnClickHandler()
    {
        MarketSystem.Instance.CloseMarket();
    }
}
