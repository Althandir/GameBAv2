using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Order : MonoBehaviour
{
    [SerializeField] SpriteContainer _spriteContainer = null;

    bool _isActive;

    bool _wantMeat;
    bool _wantCheese;
    bool _wantSalad;
    bool _wantOnion;
    bool _wantTomato;

    UnityEvent _activateEvent = new UnityEvent();
    UnityEvent _deactivateEvent = new UnityEvent();
    
    public bool IsActive { get => _isActive;}
    public bool WantMeat { get => _wantMeat; }
    public bool WantCheese { get => _wantCheese; }
    public bool WantSalad { get => _wantSalad; }
    public bool WantOnion { get => _wantOnion; }
    public bool WantTomato { get => _wantTomato; }
    public SpriteContainer SpriteContainer { get => _spriteContainer; }
    public UnityEvent ActivateEvent { get => _activateEvent; }
    public UnityEvent DeactivateEvent { get => _deactivateEvent; }

    [ContextMenu("Activate")]
    public void Activate()
    {
        if (!_isActive)
        {
            _isActive = true;
            RandomizeRequest();
            _activateEvent.Invoke();
        }
        else
        {
            Debug.Log("Order already active.");
        }
    }
    
    void RandomizeRequest()
    {

        _wantMeat = RandomBool();
        _wantCheese = RandomBool();
        _wantSalad = RandomBool();
        _wantOnion = RandomBool();
        _wantTomato = RandomBool();
    }

    bool RandomBool()
    {
        Random.InitState(Random.Range(int.MinValue, int.MaxValue));
        if (Random.value <= 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [ContextMenu("Deactivate")]
    public void Deactivate()
    {
        _isActive = false;
        _deactivateEvent.Invoke();
    }
}
