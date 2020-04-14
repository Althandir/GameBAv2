using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketUIBody : MonoBehaviour
{
    List<MarketUIAmount> amounts = new List<MarketUIAmount>();

    public List<MarketUIAmount> Amounts { get => amounts; }

    private void Awake()
    {
        InitAmountList();
    }

    void InitAmountList()
    {
        foreach (Transform child in transform)
        {
            amounts.Add(child.GetComponent<MarketUIAmount>());
        }
        amounts.RemoveAt(amounts.Count - 1);
    }

    public void ResetBody()
    {
        foreach (var UIAmount in amounts)
        {
            UIAmount.ResetAmount();
        }
    }


}
