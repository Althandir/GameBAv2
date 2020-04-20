using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class BellZone : MonoBehaviour
{
    static BellZone _instance;

    AudioSource _audioSource = null;
    UnityEvent _bellPressed = new UnityEvent();

    public UnityEvent BellPressedEvent { get => _bellPressed; }
    static public BellZone Instance { get => _instance; }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("Double BellZone found in " + gameObject.name);
        }
    }

    public void BellPressed()
    {
        if (MouseSystem.Instance.IsHolding || MouseSystem.Instance.IsHoldingKnife)
        {
            Debug.LogError("Cant press Bell. You are holding something.");
        }
        else if (!MouseSystem.Instance.enabled)
        {
            Debug.LogError("MouseSystem is Disabled.");
        }
        else if (RatSystem.Instance.IsActive)
        {
            Debug.LogError("Rat is active.");
        }
        else
        {
            Debug.Log("Bell pressed.");
            _bellPressed.Invoke();
            _audioSource.Play();
        }
    }

}
