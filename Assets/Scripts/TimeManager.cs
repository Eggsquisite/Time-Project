using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float fastForward = 2f;
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
        charManager = charPathing.GetComponent<CharacterManager>();
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
        if (Time.timeScale <= 1)
            Time.timeScale = fastForward;
        else if (Time.timeScale > 1)
            Time.timeScale = 1;
    }

    public void Rewind()
    {
        charManager.GetCharList();
    }
}
