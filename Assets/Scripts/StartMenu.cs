using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button continueButt;
    [SerializeField] GameObject controls;

    private int level = 0;
    private bool control;

    // Start is called before the first frame update
    void Start()
    {
        controls.SetActive(control);
        level = PlayerPrefs.GetInt("LevelProgress");
        if (level <= 1)
            continueButt.interactable = false;
        else if (level > 1)
            continueButt.interactable = true;
    }

    public void NewGame()
    {
        //SceneManager.LoadScene("Lvl1");
    }

    public void Continue()
    {
        Debug.Log(PlayerPrefs.GetInt("LevelProgress"));
        if (level > 1) ;
            //SceneManager.LoadScene(level);
    }

    public void LoadScene()
    { 
        //SceneManager.LoadScene("LoadScene");
    }

    public void Controls()
    {
        control = !control;
        controls.SetActive(control);
    }
}
