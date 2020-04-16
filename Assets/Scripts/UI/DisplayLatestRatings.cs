using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLatestRatings : DisplayRating
{
    [SerializeField] Transform _starsContainer = null;
    [SerializeField] TMP_Text _textComponent = null;
    [SerializeField] bool _getLatestRating = false;

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
        _textComponent.text = LanguageManager.Instance.GetRatingText(RatingAsInt);
    }
}
