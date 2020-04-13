using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    #region Instance
    static Player _instance;
    public static Player Instance { get => _instance; }
    #endregion

    #region Private values
    readonly UnityEvent _onRatingChange = new UnityEvent();

    List<Rating> _ratings = new List<Rating>();
    Rating _totalRating = new Rating();
    
    MouseSystem _mouseDragger;
    #endregion

    #region Properties

    public UnityEvent OnRatingChange { get => _onRatingChange; }
    public MouseSystem MouseDragger { get => _mouseDragger; }
    public Rating TotalRating { get => _totalRating; }
    public int LatestRatingAsInt
    { get 
        { 
            if (_ratings.Count > 0) 
                return (int)_ratings[_ratings.Count - 1];
            else
            {
                return -1;
            }
        } 
    }
    public int SecondLatestRatingAsInt
    {
        get
        {
            if (_ratings.Count > 1)
                return (int)_ratings[_ratings.Count - 2];
            else
            {
                return -1;
            }
        }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            _mouseDragger = transform.GetChild(1).GetComponent<MouseSystem>();
        }
        else
        {
            Debug.LogError("Double PlayerScript in Scene!");
            Destroy(this);
        }
    }

    private void Start()
    {
        _onRatingChange.Invoke();
    }

    #endregion

    public void AddRating(Rating newRating)
    {
        _ratings.Add(newRating);
        CalcTotalRating();
        _onRatingChange.Invoke();
    }

    void CalcTotalRating()
	{
        if (_ratings.Count > 5)
        {
            int temp = 0;
            foreach (Rating rating in _ratings)
            {
                temp += (int) rating;
            }
            _totalRating = (Rating) (temp / _ratings.Count);
        }
        else
        {
            _totalRating = Rating.zeroStar;
        }
	}
}
