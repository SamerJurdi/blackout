using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Sound : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;

    void playSound()
    {
        _audioSource.PlayOneShot(_audioClip);
    }
}
