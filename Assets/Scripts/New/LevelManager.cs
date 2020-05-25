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
        scene = SceneManager.GetActiveScene();
    }

    public void SaveLevel()
    { 
        
    }

    public void LoadNextLevel()
    {
        level++;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}
