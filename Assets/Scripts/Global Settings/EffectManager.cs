using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Material _glowStars = null;

    private void Update()
    {
        PulsateStars();
    }

    void PulsateStars()
    {
        _glowStars.SetFloat("_GlowIntensity", Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup)));
    }
}
