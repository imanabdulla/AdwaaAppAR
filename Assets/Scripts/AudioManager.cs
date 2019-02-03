using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip[] AfterCheckingAnswer;

    private void Awake()
    {
        AM = this;
        audioSource = AM.GetComponent<AudioSource>();
    }
}
