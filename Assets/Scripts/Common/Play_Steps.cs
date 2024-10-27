using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Steps : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;

    void playSteps()
    {
        _audioSource.PlayOneShot(_audioClip);
    }
}
