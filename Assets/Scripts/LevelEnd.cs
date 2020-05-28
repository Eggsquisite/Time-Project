using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject uiBackground, lvlOver, lvlOverShadow, retryLvl, nextLvl;
    [SerializeField] AudioClip victoryAudio = null;
    [SerializeField] AudioClip failAudio = null;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        EnableText(false, -1);
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void SuccessText()
    {
        lvlOver.GetComponent<Text>().text = "All Townsfolk Alive!";
        lvlOverShadow.GetComponent<Text>().text = "All Townsfolk Alive!";
        audioSource.PlayOneShot(victoryAudio);
        EnableText(true, 1);
    }

    public void FailureText()
    {
        lvlOver.GetComponent<Text>().text = "Townsfolk died!";
        lvlOverShadow.GetComponent<Text>().text = "Townsfolk died!";
        PlayClip(failAudio);
        EnableText(true, 0);
        
    }

    private void EnableText(bool status, int success)
    {
        uiBackground.SetActive(status);
        lvlOver.SetActive(status);

        if (success == 0)
            retryLvl.SetActive(status);
        else if (success == 1)
            nextLvl.SetActive(status);
    }

    private void PlayClip(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
        audioSource.loop = false;
    }
}
