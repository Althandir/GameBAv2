using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static GameManager _instance;

    public static GameManager Instance { get => _instance; }

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
        
    }



}
