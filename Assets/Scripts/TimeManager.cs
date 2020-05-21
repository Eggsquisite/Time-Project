using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float speedUp = 2f;
    [SerializeField] float rewindTime = -1f;

    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

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
            Time.timeScale = speedUp;
        else if (Time.timeScale > 1)
            Time.timeScale = 1;
    }

    public void Rewind()
    {
        if (Time.timeScale >= 0)
            Time.timeScale = rewindTime;
        else if (Time.timeScale < 0)
            Time.timeScale = 1;
    }
}
