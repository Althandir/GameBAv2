using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CuttingKnife : MonoBehaviour
{
    [SerializeField] bool _hasKnife;
    Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeStatus()
    {
        if (_hasKnife)
        {
            _hasKnife = false;
            _image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        else
        {
            _hasKnife = true;
            _image.color = Color.white;
        }
    }
}
