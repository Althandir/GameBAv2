using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MouseSystemAudioManager : MonoBehaviour
{
    [SerializeField][Range(0,1)] float _volumeScale = 0.5f;

    [Header("AudioClips:")]
    [SerializeField] AudioClip _onDropFoodSound = null;
    [SerializeField] AudioClip _onDropKnifeSound = null;
    [SerializeField] AudioClip _onChopFoodSound = null;
    [SerializeField] AudioClip _onPickupFoodSound = null;
    [SerializeField] AudioClip _onPickupKnifeSound = null;
    [SerializeField] AudioClip _onError = null;

    AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDropSound()
    {
        StopPlaying();
        _audioSource.PlayOneShot(_onDropFoodSound, _volumeScale);
    }

    public void PlayDropKnifeSound()
    {
        StopPlaying();
        _audioSource.PlayOneShot(_onDropKnifeSound, _volumeScale);
    }

    public void PlayOnChopFoodSound()
    {
        StopPlaying();
        _audioSource.PlayOneShot(_onChopFoodSound, _volumeScale);
    }

    public void PlayOnPickupFoodSound()
    {
        StopPlaying();
        _audioSource.PlayOneShot(_onPickupFoodSound, _volumeScale);
    }

    public void PlayOnErrorSound()
    {
        StopPlaying();
        _audioSource.PlayOneShot(_onError, _volumeScale);
    }

    void StopPlaying()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    public void PlayOnPickupKnifeSound()
    {
        StopPlaying();
        _audioSource.PlayOneShot(_onPickupKnifeSound, _volumeScale);
    }
}
