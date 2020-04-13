using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRating : MonoBehaviour
{
    [SerializeField] protected Sprite _coloredStar = null;
    [SerializeField] protected Sprite _blankStar = null;
    [SerializeField] protected Material _glow = null;
    protected List<Image> _stars = new List<Image>();

    virtual protected void Awake()
    {
        foreach (Transform transform in transform)
        {
            _stars.Add(transform.gameObject.GetComponent<Image>());
        }
    }

    virtual protected void Start()
    {
        Player.Instance.OnRatingChange.AddListener(HandleRatingChange);
    }

    virtual protected void HandleRatingChange()
    {
        int RatingAsInt = (int)Player.Instance.TotalRating;

        ChangeDisplayedStars(RatingAsInt);
    }

    protected void ChangeDisplayedStars(int num)
    {
        if (num < 0)
        {
            num = 0;
        }

        for (int i = 0; i < num; i++)
        {
            _stars[i].sprite = _coloredStar;
            _stars[i].material = _glow;
        }
        for (int i = num; i < _stars.Count; i++)
        {
            _stars[i].sprite = _blankStar;
            _stars[i].material = null;
        }
    }
}
