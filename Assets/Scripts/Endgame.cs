using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] AudioClip victory;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == -7.5f)
        {
            if (text != null && !text.activeSelf)
            {
                text.SetActive(true);
                audioSource.PlayOneShot(victory);
            }
        }
    }
}
