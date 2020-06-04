using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null; 
    [SerializeField] GameObject uiBackground = null;
    [SerializeField] Image image = null;

    private bool paused;
    private Color tmp;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        tmp = image.color;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
            paused = false;

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !uiBackground.activeSelf && !paused)
            Paused();
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && paused)
            Unpause();
    } 

    private void Paused()
    {
        paused = true;
        pauseMenu.SetActive(true);

        tmp.a = 0.3f;
        image.color = tmp;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        paused = false;
        pauseMenu.SetActive(false);

        tmp.a = 0f;
        image.color = tmp;
        Time.timeScale = 1f;
    }
}
