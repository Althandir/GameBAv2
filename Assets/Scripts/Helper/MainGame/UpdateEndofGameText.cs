using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateEndofGameText : MonoBehaviour
{
    [SerializeField] TMP_Text endText = null;

    private void Awake()
    {
        endText.text = LanguageManager.Instance.GetEndOfGameText();
    }
}
