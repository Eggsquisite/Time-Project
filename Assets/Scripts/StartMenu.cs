using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button continueButt = null;
    [SerializeField] Button nextButt = null;
    [SerializeField] GameObject menuButtons, titleText, controls, controlList, credits, loadLvl;

    private List<Transform> theList;
    private int level, index;
    private bool control, title, creditsOn, loadLvlOn;

    private void Awake()
    {
        title = true;
        titleText.SetActive(true);
        menuButtons.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        theList = GetControlList();
        controls.SetActive(control);
        credits.SetActive(creditsOn);
        loadLvl.SetActive(loadLvlOn);

        level = PlayerPrefs.GetInt("LevelProgress");
        if (level < 2)
            continueButt.interactable = false;
        else 
            continueButt.interactable = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && title)
        {
            title = false;
            titleText.SetActive(false);
            menuButtons.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (Time.timeScale < 1)
            Time.timeScale = 1f;
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

    private void ResetTutorialText()
    {
        PlayerPrefs.SetInt("portalText", 0);
        PlayerPrefs.SetInt("hiddenText", 0);
        PlayerPrefs.SetInt("edText", 0);
        PlayerPrefs.SetInt("roseText", 0);
        PlayerPrefs.SetInt("oscarText", 0);
    }

    public void NewGame()
    {
        ResetTutorialText();
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

    public void LoadScreen()
    {
        loadLvlOn = !loadLvlOn;
        loadLvl.SetActive(loadLvlOn);
    }

    public void LoadLevel(int lvl)
    {
        ResetTutorialText();
        StartCoroutine(AsyncLoadLevel(lvl));
    }

    IEnumerator AsyncLoadLevel(int lvl)
    {
        AsyncOperation loadGame = SceneManager.LoadSceneAsync(lvl);
        while (!loadGame.isDone)
        {
            yield return null;
        }
    }

    public void Controls()
    {
        controls.SetActive(true);
    }

    public void Credits()
    {
        creditsOn = !creditsOn;
        credits.SetActive(creditsOn);
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
