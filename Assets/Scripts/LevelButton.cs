using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] GameObject controls = null;
    LevelManager lm = null;
    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retrylevel();
        }
    }

    public void LoadNextLevel()
    {
        lm.LoadNextLevel();
    }

    public void Retrylevel()
    {
        lm.RetryLevel();
    }

    public void Controls()
    {
        controls.SetActive(true);
    }

    public void MainMenu()
    {
        lm.MainMenu();
    }

    public void QuitGame()
    {
        //lm.SaveLevel();
        Application.Quit();
    }
}
