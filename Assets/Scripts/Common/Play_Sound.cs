using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Sound : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] [Range(0, 100)] float playChance = 100f;

    private Audio_Controller _audioManager;

    private void Awake() {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Controller>();
    }

    void playSound()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= playChance)
        {
            _audioManager.PlaySFX(_audioClip);
        }
    }
}
