using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DisplayLatestRatings : DisplayRating
{
    [SerializeField] Transform _starsContainer;
    [SerializeField] TMP_Text _textComponent;
    [SerializeField] bool _getLatestRating;

    override protected void Awake()
    {
        foreach (Transform child in _starsContainer)
        {
            _stars.Add(child.GetComponent<Image>());
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void HandleRatingChange()
    {
        int RatingAsInt;
        if (_getLatestRating)
        {
            RatingAsInt = Player.Instance.LatestRatingAsInt;
        }
        else
        {
            RatingAsInt = Player.Instance.SecondLatestRatingAsInt;
        }

        ChangeDisplayedStars(RatingAsInt);
        ChangeText(RatingAsInt);
    }

    private void ChangeText(int RatingAsInt)
    {
        switch (RatingAsInt)
        {
            case 0:
                _textComponent.text = "0 Sterne.";
                break;
            case 1:
                _textComponent.text = "1 Stern.";
                break;
            case 2:
                _textComponent.text = "2 Sterne.";
                break;
            case 3:
                _textComponent.text = "3 Sterne.";
                break;
            case 4:
                _textComponent.text = "4 Sterne.";
                break;
            case 5:
                _textComponent.text = "5 Sterne.";
                break;
            default:
                _textComponent.text = string.Empty;
                break;
        }
    }
}
