using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Steps : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;

    private Audio_Controller _audioManager;

    private void Awake() {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Controller>();
    }

    void playSteps()
    {
        _audioManager.PlaySFX(_audioClip);
    }
}
