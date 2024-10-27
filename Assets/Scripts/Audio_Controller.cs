using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Controller : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] AudioClip backgroundAmbience;
    [SerializeField] AudioClip crows;
    [SerializeField] AudioClip treeCreaking;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    void Start()
    {
        musicSource.clip = backgroundAmbience;
        musicSource.Play();
    }

}
