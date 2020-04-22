using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSettings : MonoBehaviour
{
    static GameSettings _instance;

    UnityEvent _onValueChanged = new UnityEvent();

    float _ratDelay = 10;
    int _numMaxOrders = 20;

    public static GameSettings Instance { get => _instance;}
    public UnityEvent OnValueChanged { get => _onValueChanged; }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float RatDelay 
    { get => _ratDelay;
        set
        {
            _ratDelay = value;
            _onValueChanged.Invoke();
        }
    }
    public int NumMaxOrders 
    { get => _numMaxOrders; 
        set 
        {
            _numMaxOrders = value;
            _onValueChanged.Invoke(); 
        }
    }

    public void AddNumMaxOrder()
    {
        if (NumMaxOrders < 40)
        {
            NumMaxOrders += 1;
        }
    }

    public void DecNumMaxOrder()
    {
        if (NumMaxOrders > 5)
        {
            NumMaxOrders -= 1;
        }
    }

    public void AddRatDelay()
    {
        if (RatDelay < 30)
        {
            RatDelay += 1;
        }
    }

    public void DecRatDelay()
    {
        if (RatDelay > 10)
        {
            RatDelay -= 1;
        }
    }


}
