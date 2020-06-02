using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    [SerializeField] List<AudioClip> music = null;
    [SerializeField] bool startMenu;

    private AudioSource audioSource;
    private int level;
    private bool audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetFloat("audioLevel") > 0)
            audioSource.volume = PlayerPrefs.GetFloat("audioLevel");
        else if (startMenu)
        {
            audioSource.volume = 0.25f;
            audioPlay = true;
            audioSource.Play();
        }
        else
        {
            audioSource.volume = 0.25f;
            audioPlay = true;
            audioSource.Play();
        }

        //PlayerPrefs.SetFloat("audioLevel", 0);
        if (PlayerPrefs.GetFloat("audioLevel") == 0.25f && !startMenu)
        {
            Debug.Log("stopping " + audioPlay);
            audioSource.Stop();
            audioPlay = false;
            audioSource.playOnAwake = false;
        }
        else if (PlayerPrefs.GetFloat("audioLevel") == 0.26f && !startMenu)
        {
            audioSource.playOnAwake = true;
            audioPlay = true;
            audioSource.Play();
        }
        else
            audioSource.Play();

        level = SceneManager.GetActiveScene().buildIndex;
        if (level % 1 == 0 && audioPlay)
        {
            var num = Random.Range(0, music.Count);
            var tmp = music[num];
            audioSource.clip = tmp;
            audioSource.Play();
        }
    }

}
