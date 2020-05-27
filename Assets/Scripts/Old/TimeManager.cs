using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float timeAlt = 0.25f;
    private float timeScaleVar = 1;
    GameObject charPathing;
    CharacterManager charManager = null;

    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Start()
    {
        charPathing = GameObject.Find("Characters");
        //charManager = charPathing.GetComponent<CharacterManager>();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1;
    }

    public void FastForward()
    {
        if (Time.timeScale >= 2)
            return;
        else
        {
            timeScaleVar += timeAlt;
            Time.timeScale = timeScaleVar;
        }

        Debug.Log(timeScaleVar + "FF");
    }

    public void Rewind()
    {
        //charManager.GetCharList();
        if (Time.timeScale <= 0.25)
            return;
        else
        {
            timeScaleVar -= timeAlt;
            Time.timeScale = timeScaleVar;
        }

        Debug.Log(timeScaleVar + "Slowdown");
    }
}
