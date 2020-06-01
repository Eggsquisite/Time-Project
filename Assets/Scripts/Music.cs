using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    [SerializeField] List<AudioClip> music = null;

    private AudioSource audioSource;
    private int level;
    private bool audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetFloat("audioLevel") > 0)
            audioSource.volume = PlayerPrefs.GetFloat("audioLevel");
        else
        {
            audioSource.volume = 0.25f;
            audioPlay = true;
        }

        //PlayerPrefs.SetFloat("audioLevel", 0);
        if (PlayerPrefs.GetFloat("audioLevel") == 0.25f)
        {
            audioSource.Stop();
            audioPlay = false;
            audioSource.playOnAwake = false;
        }
        else if (PlayerPrefs.GetFloat("audioLevel") == 0.26f)
        {
            audioSource.playOnAwake = true;
            audioPlay = true;
        }
        else
        {
            audioSource.Play();
        }

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
