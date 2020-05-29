using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button continueButt = null;
    [SerializeField] Button nextButt = null;
    [SerializeField] Button loadLvlButt = null;
    [SerializeField] GameObject menuButtons, titleText, controls, controlList;

    private List<Transform> theList;
    private int level, index;
    private bool control, title;

    private void Awake()
    {
        title = true;
        menuButtons.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        theList = GetControlList();
        loadLvlButt.interactable = false;
        controls.SetActive(control);

        level = PlayerPrefs.GetInt("LevelProgress");
        if (level < 2)
            continueButt.interactable = false;
        else 
            continueButt.interactable = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        else if (Input.anyKeyDown && title)
        {
            title = !title;
            titleText.SetActive(false);
            menuButtons.SetActive(true);
        }
    }

    private List<Transform> GetControlList()
    {
        var tmp = new List<Transform>();

        foreach (Transform child in controlList.transform)
        {
            tmp.Add(child);
        }
        return tmp;
    }

    public void NewGame()
    {
        StartCoroutine(AsyncNewGame());
    }

    IEnumerator AsyncNewGame()
    {
        AsyncOperation newGame = SceneManager.LoadSceneAsync("Lvl1");
        while (!newGame.isDone)
        {
            yield return null;
        }
    }

    public void Continue()
    {
        Debug.Log(PlayerPrefs.GetInt("LevelProgress"));
        if (level > 1)
            StartCoroutine(AsyncContinueLevel());
    }

    IEnumerator AsyncContinueLevel()
    {
        AsyncOperation contGame = SceneManager.LoadSceneAsync(level);
        while (!contGame.isDone)
        {
            yield return null;
        }
    }

    public void LoadLevel()
    { 
        //
    }

    public void Controls()
    {
        controls.SetActive(true);
    }

    public void Next()
    {
        if (index < theList.Count - 1)
        {
            theList[index].gameObject.SetActive(false);
            index++;
            theList[index].gameObject.SetActive(true);
        }
        
        if (index >= theList.Count - 1)
            nextButt.interactable = false;
    }

    public void Back()
    {
        if (index == 0)
            controls.SetActive(false);
        else
        {
            nextButt.interactable = true;
            theList[index].gameObject.SetActive(false);
            index--;
            theList[index].gameObject.SetActive(true);
        }
    }
}
