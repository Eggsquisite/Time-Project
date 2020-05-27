using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    LevelManager lm;
    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
    }

    public void LoadNextLevel()
    {
        lm.LoadNextLevel();
    }

    public void Retrylevel()
    {
        lm.RetryLevel();
    }
}
