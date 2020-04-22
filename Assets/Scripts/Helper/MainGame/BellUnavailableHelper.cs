using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BellUnavailableHelper : MonoBehaviour
{
    Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (RatSystem.Instance.IsActive && !_image.enabled)
        {
            _image.enabled = true;
        }
        else if (!RatSystem.Instance.IsActive && _image.enabled)
        {
            _image.enabled = false;
        }
    }
}
