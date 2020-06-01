﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject uiBackground, lvlOver, lvlOverShadow, retryLvl, nextLvl;
    [SerializeField] AudioClip victoryAudio = null;
    [SerializeField] AudioClip failAudio = null;
    [SerializeField] float timeScale = 1f;
    [SerializeField] bool endGame;

    private AudioSource audioSource;
    private bool oneEnding, end;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = timeScale;
        audioSource = Camera.main.GetComponent<AudioSource>();

        if (!endGame)
            EnableText(false, -1);
    }

    private void Update()
    {
        
        if (endGame && uiBackground.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void SuccessText()
    {
        if (!oneEnding && !endGame)
        {
            lvlOver.GetComponent<Text>().text = "All Townsfolk Alive!";
            lvlOverShadow.GetComponent<Text>().text = "All Townsfolk Alive!";
            audioSource.PlayOneShot(victoryAudio);
            EnableText(true, 1);
            oneEnding = true;
        }
    }

    public void FailureText()
    {
        if (!oneEnding)
        {
            lvlOver.GetComponent<Text>().text = "Townsfolk died!";
            lvlOverShadow.GetComponent<Text>().text = "Townsfolk died!";
            PlayClip(failAudio);
            EnableText(true, 0);
            oneEnding = true;
        }
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
