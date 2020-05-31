using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{

    [SerializeField] AudioClip audioClip = null;

    private AudioSource myAudio;
    private bool audioOn = true;

    private void Start()
    {
        myAudio = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        myAudio.PlayOneShot(audioClip);
    }

    public void SwitchMusic()
    {
        if (audioOn || PlayerPrefs.GetFloat("audioLevel") == 0.26f)
        {
            myAudio.Stop();
            PlayerPrefs.SetFloat("audioLevel", 0.25f);
            audioOn = !audioOn;
        }
        else if (!audioOn || PlayerPrefs.GetFloat("audioLevel") == 0.25f)
        {
            myAudio.Play();
            PlayerPrefs.SetFloat("audioLevel", 0.26f);
            audioOn = !audioOn;
        }
    }
}
