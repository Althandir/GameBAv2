using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    [SerializeField] GameObject _endofGameUI = null;
    [SerializeField] OrderZone _orderZone = null;
    [SerializeField] RatSystem _ratSystem = null;
    [SerializeField] HighlightManager _highlightManager = null;
    [SerializeField] int _maxNumOrders = 3;
    [SerializeField] float _ratSpawnDelay = 30; 

    int _actNumOrders = 0;
    bool _endOfGameReached;

    float _gametime = 0.0f;
    float _ratTimer = 0.0f;

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

        LoadSettings();
    }

    private void Start()
    {
        _orderZone.NewOrderCreatedEvent.AddListener(OnNewOrder);
    }

    private void Update()
    {
        CheckRatSystem();
        if (!_endOfGameReached)
        {
            _gametime += Time.deltaTime;
        }

        if (CheckEndGameConditions() && !_endOfGameReached)
        {
            EndGame();
        }
    }

    private void LoadSettings()
    {
        if (GameSettings.Instance)
        {
            _maxNumOrders = GameSettings.Instance.NumMaxOrders;
            _ratSpawnDelay = GameSettings.Instance.RatDelay;
        }
    }

    private void CheckRatSystem()
    {
        _ratTimer += Time.deltaTime;
        if (_ratTimer > _ratSpawnDelay)
        {
            _ratTimer = 0.0f;
            _ratSystem.SpawnRat();
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
        _endOfGameReached = true;
        _highlightManager.enabled = false;
        _endofGameUI.SetActive(true);
        SaveData.Save.All(_gametime, Player.Instance.TotalRating);
        Debug.Log("End of Game!");
    }
}
