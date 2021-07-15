using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private Scene scene;
    private int level = 0;

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
        SaveLevel();
        Debug.Log("Furthest lvl: " + PlayerPrefs.GetInt("LevelProgress"));
        Debug.Log("Current level: " + level);
    }

    public void SaveLevel()
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
        //Destroy(Camera.main);
        GameManager.instance.SetObserveMode(true);
        StartCoroutine(AsyncNextLevel());
    }

    IEnumerator AsyncNextLevel()
    {
        AsyncOperation nextLevel = SceneManager.LoadSceneAsync(level + 1);
        while (!nextLevel.isDone)
        {
            yield return null;
        }
    }

    public void MainMenu()
    {
        StartCoroutine(AsyncMainMenu());
    }

    IEnumerator AsyncMainMenu()
    {
        AsyncOperation mainMenu = SceneManager.LoadSceneAsync("StartMenu");
        while (!mainMenu.isDone)
        {
            yield return null;
        }
    }

    public void RetryLevel()
    {
        //DontDestroyOnLoad(Camera.main);
        GameManager.instance.SetObserveMode(true);
        StartCoroutine(AsyncRetryLevel());
    }

    IEnumerator AsyncRetryLevel()
    {
        AsyncOperation retryLevel = SceneManager.LoadSceneAsync(level);
        while (!retryLevel.isDone)
        {
            yield return null;
        }
    }
}
