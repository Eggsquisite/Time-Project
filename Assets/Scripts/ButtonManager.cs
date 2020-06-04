﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] AudioClip scrollSound = null;
    [SerializeField] Text text = null;

    private Button b;
    private ObjectPlacement placer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        placer = Camera.main.GetComponent<ObjectPlacement>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void SetSelected(GameObject portal, Button button)
    {
        placer.SetSelected(portal);
        b = button;
        if (text != null)
            text.text = "Right Click-> Unselect";

        audioSource.PlayOneShot(scrollSound);
    }

    public void Reset()
    {
        b.interactable = true;
        if (text != null)
            text.text = "Left Click-> Select";
    }
}
