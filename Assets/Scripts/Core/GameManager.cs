using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    [SerializeField] OrderZone _orderZone;
    [SerializeField] RatSystem _ratSystem;
    [SerializeField] HighlightManager _highlightManager;

    [SerializeField] int _maxNumOrders = 3;
    int _actNumOrders = 0;

    bool _endGame;

    UnityEvent _maxOrdersReached = new UnityEvent();

    public static GameManager Instance { get => _instance; }
    public UnityEvent MaxOrdersReachedEvent { get => _maxOrdersReached; }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple GameManagers found!");
        }
    }

    private void Start()
    {
        _orderZone.NewOrderCreatedEvent.AddListener(OnNewOrder);
    }

    private void Update()
    {
        if (CheckEndGameConditions())
        {
            EndGame();
        }
    }

    private void OnNewOrder()
    {
        _actNumOrders += 1;
        if (_actNumOrders >= _maxNumOrders)
        {
            _maxOrdersReached.Invoke();
        }
    }

    bool CheckEndGameConditions()
    {
        if (_actNumOrders >= _maxNumOrders && !_orderZone.HasActiveOrders)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EndGame()
    {
        if (!_endGame)
        {
            _endGame = true;
            _highlightManager.enabled = false;
            Debug.Log("End of Game!");
        }
    }
}
