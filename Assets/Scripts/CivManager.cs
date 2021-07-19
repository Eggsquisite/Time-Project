﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivManager : MonoBehaviour
{

    [SerializeField] Transform civs = null;
    [SerializeField] float deathTime = 1f;
    private LevelEnd lvlEnd;

    private void Start()
    {
        lvlEnd = FindObjectOfType<LevelEnd>();    
    }

    public void Failure()
    {
        StartCoroutine(Failed());
    }

    public void Success()
    {
        lvlEnd.SuccessText();
    }

    private IEnumerator Failed()
    { 
        yield return new WaitForSeconds(deathTime);
        lvlEnd.FailureText();
    }

    public int GetNumOfCivs()
    {
        int tmp = 0;

        foreach (Transform child in transform) {
            if (child.GetComponent<Civvie>() != null && child.gameObject.activeSelf == true)
                tmp++;
        }

        Debug.Log("Number of civilians needed: " + tmp);
        return tmp;
    }
}
