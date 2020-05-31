using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Continue();
        

        //if (PlayerPrefs.GetInt("EdText") != 1)
            //Continue();
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("EdText", 1);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
