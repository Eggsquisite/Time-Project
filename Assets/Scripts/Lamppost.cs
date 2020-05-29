﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamppost : MonoBehaviour
{

    [SerializeField] Animator lightAnim = null;
    [SerializeField] GameObject magicWall = null;

    private LevelEnd lvlEnd;
    private CivManager civs;
    private int numOfCivs;
    private float rescued = 0;

    // Start is called before the first frame update
    void Start()
    {
        lvlEnd = FindObjectOfType<LevelEnd>();
        civs = FindObjectOfType<CivManager>();
        numOfCivs = civs.GetNumOfCivs();
        magicWall.SetActive(false);
    }

    private void Success()
    {
        lightAnim.SetBool("lvlSuccess", true);
        lvlEnd.SuccessText();
        magicWall.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            rescued++;

        if (rescued == numOfCivs)
            Success();
    }
}
