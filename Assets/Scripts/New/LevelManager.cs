using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    Scene scene;
    public int level = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ResetProgress();
        scene = SceneManager.GetActiveScene();
    }

    public void NewScene()
    {
        scene = SceneManager.GetActiveScene();
        level = scene.buildIndex;
        //SaveLevel();
        Debug.Log("Furthest lvl: " + PlayerPrefs.GetInt("LevelProgress"));
        Debug.Log("Current level: " + level);
    }

    private void SaveLevel()
    {
        if (PlayerPrefs.GetInt("LevelProgress") < level)
            PlayerPrefs.SetInt("LevelProgress", level);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("LevelProgress", 0);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(level + 1);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(level);
    }
}
