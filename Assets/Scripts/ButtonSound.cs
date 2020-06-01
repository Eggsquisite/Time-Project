using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{

    [SerializeField] AudioClip audioClip = null;

    private AudioSource myAudio;
    private bool audioOn;

    private void Start()
    {
        myAudio = Camera.main.GetComponent<AudioSource>();
        if (myAudio.isPlaying)
            audioOn = true;
    }

    public void PlayAudio()
    {
        myAudio.PlayOneShot(audioClip);
    }

    public void SwitchMusic()
    {
        if (myAudio.isPlaying || PlayerPrefs.GetFloat("audioLevel") == 0.26f)
        {
            myAudio.Stop();
            PlayerPrefs.SetFloat("audioLevel", 0.25f);
            audioOn = false;
        }
        else if (!myAudio.isPlaying || PlayerPrefs.GetFloat("audioLevel") == 0.25f)
        {
            myAudio.Play();
            PlayerPrefs.SetFloat("audioLevel", 0.26f);
            audioOn = true;
        }
    }
}
