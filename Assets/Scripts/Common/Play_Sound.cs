using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Sound : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] [Range(0, 100)] float playChance = 100f;

    void playSound()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= playChance)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
