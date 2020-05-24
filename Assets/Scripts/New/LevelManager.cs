using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(scene.buildIndex + 1, LoadSceneMode.Single);
    }
}
