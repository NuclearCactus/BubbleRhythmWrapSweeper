using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodToAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio()
    {
        audioSource.Play();
    }
}
