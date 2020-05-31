using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    [SerializeField] GameObject civs = null, enemies = null;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0f;
        civs.SetActive(false);
        enemies.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            Continue();

        //if (PlayerPrefs.GetInt("EdText") != 1)
            //Continue();
    }

    public void Continue()
    {
        //PlayerPrefs.SetInt("EdText", 1);
        //Time.timeScale = 1f;
        civs.SetActive(true);
        enemies.SetActive(true);
        Destroy(gameObject);
    }
}
