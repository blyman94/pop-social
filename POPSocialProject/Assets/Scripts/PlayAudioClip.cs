using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper that allows access to the PlayOneShot method of the AudioSource
/// component. Useful for playing sound effects during animation events.
/// </summary>
public class PlayAudioClip : MonoBehaviour
{
    /// <summary>
    /// AudioSource through which the clip will be played.
    /// </summary>
    [Tooltip("AudioSource through which the clip will be played.")]
    [SerializeField] private AudioSource _audioSource;

    /// <summary>
    /// AudioClip that will be played by default.
    /// </summary>
    [Tooltip("AudioClip that will be played by default.")]
    [SerializeField] private AudioClip _toPlay;

    /// <summary>
    /// Minimum pitch to play clip at for random pitch.
    /// </summary>
    [Tooltip("Minimum pitch to play clip at for random pitch.")]
    [SerializeField] private float _pitchMin = 0.9f;

    /// <summary>
    /// Maximum pitch to play clip at for random pitch.
    /// </summary>
    [Tooltip("Maximum pitch to play clip at for random pitch.")]
    [SerializeField] private float _pitchMax = 1.10f;

    /// <summary>
    /// Plays the default AudioClip at a random pitch between _pitchMin and
    /// _pitchMax.
    /// </summary>
    public void PlayAtRandomPitch()
    {
        _audioSource.pitch = Random.Range(_pitchMin, _pitchMax);
        _audioSource.PlayOneShot(_toPlay);
    }
}
