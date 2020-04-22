using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderZone))]
public class HighlightManager : MonoBehaviour
{
    OrderZone _orderZone;
    [SerializeField] Image[] _highlighterImages = null;

    private void Awake()
    {
        _orderZone = GetComponent<OrderZone>();
    }

    private void Start()
    {
        _orderZone.ChangeNextOrderIDEvent.AddListener(OnChangeNextID);
    }

    private void FixedUpdate()
    {
        Pulsate();
    }

    public void OnDisable()
    {
        foreach (var image in _highlighterImages)
        {
            image.enabled = false;
        }
    }

    void Pulsate()
    {
        foreach (var image in _highlighterImages)
        {
            image.color = new Color(0, Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup)), Mathf.Abs(Mathf.Cos(Time.realtimeSinceStartup)), 1);
        }
    }

    void OnChangeNextID(int id)
    {
        foreach (var image in _highlighterImages)
        {
            image.enabled = false;
        }

        _highlighterImages[id].enabled = true;
    }
}
