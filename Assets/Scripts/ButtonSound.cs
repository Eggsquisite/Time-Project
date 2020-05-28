using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{

    [SerializeField] AudioClip audioClip = null;

    private AudioSource myAudio;

    private void Start()
    {
        myAudio = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        myAudio.PlayOneShot(audioClip);
    }
}
