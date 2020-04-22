using System;
using UnityEngine;
using TMPro;

public class UpdateActualValueHelper : MonoBehaviour
{
    enum WatchableValue
    {
        NumOrder, RatDelay
    }


    [SerializeField] WatchableValue watchableValue = new WatchableValue();
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        GameSettings.Instance.OnValueChanged.AddListener(UpdateValue);
        UpdateValue();
    }

    private void UpdateValue()
    {
        switch (watchableValue)
        {
            case WatchableValue.NumOrder:
                text.text = GameSettings.Instance.NumMaxOrders.ToString();
                break;
            case WatchableValue.RatDelay:
                text.text = GameSettings.Instance.RatDelay.ToString();
                break;
            default:
                break;
        }
    }
}
