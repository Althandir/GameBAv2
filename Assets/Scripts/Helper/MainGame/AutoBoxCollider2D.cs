using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D), typeof(RectTransform))]
public class AutoBoxCollider2D : MonoBehaviour
{
    RectTransform _rt;
    BoxCollider2D _col2D;
    [Tooltip("Change this Value to update Colliders in Editor. Not used ingame.")]
    [SerializeField] bool _UpdateNow;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _col2D = GetComponent<BoxCollider2D>();
        SetupBoxCollider2D();
    }

    private void OnValidate()
    {
        Awake();
    }

    // Idea from: 
    // https://answers.unity.com/questions/926039/how-to-match-boxcollider2d-size-with-image-after-p.html
    void SetupBoxCollider2D()
    {
        _col2D.size = _rt.rect.size;
    }

}
