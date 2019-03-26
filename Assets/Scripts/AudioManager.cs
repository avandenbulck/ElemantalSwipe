using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource vunerableHitSound;
    public AudioSource resistantHitSound;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }  
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void PlayVunerableHitSound()
    {
        PlaySourceOneShot(vunerableHitSound);
    }

    public void PlayResistantHitSound()
    {
        PlaySourceOneShot(resistantHitSound);
    }

    public void PlaySourceOneShot(AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
