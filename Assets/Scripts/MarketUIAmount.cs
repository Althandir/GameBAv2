using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketUIAmount : MonoBehaviour
{
    [SerializeField] Food _type = new Food();
    int _amount;
    [SerializeField] TMP_Text _text = null;

    public Food Type { get => _type; }
    public int Amount { get => _amount; }

    /// <summary>
    /// Increases Amount by one and syncronices with the UI
    /// </summary>
    public void IncAmount()
    {
        if (_amount < 10)
        {
            _amount += 1;
            UpdateText();
        }
    }

    /// <summary>
    /// Decreases Amount by one and syncronices with the UI
    /// </summary>
    public void DecAmount()
    {
        if (_amount > 0)
        {
            _amount -= 1;
            UpdateText();
        }
    }
    /// <summary>
    /// Resets Amount to 0 and syncronices with the UI
    /// </summary>
    public void ResetAmount()
    {
        _amount = 0;
        UpdateText();
    }

    void UpdateText()
    {
        _text.text = _amount.ToString();
    }

}
