using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseText;

    private bool paused;
    private Color tmp;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        pauseText.SetActive(false);
        image = GetComponent<Image>();
        tmp = image.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            Paused();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            Unpause();
    }

    private void Paused()
    {
        paused = true;
        pauseText.SetActive(true);

        tmp.a = 0.3f;
        image.color = tmp;
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        paused = false;
        pauseText.SetActive(false);

        tmp.a = 0f;
        image.color = tmp;
        Time.timeScale = 1f;
    }
}
