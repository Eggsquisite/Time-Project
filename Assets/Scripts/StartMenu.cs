using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button continueButt;
    [SerializeField] Button nextButt;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject controlList;

    private List<Transform> theList;
    private int level, index;
    private bool control;

    // Start is called before the first frame update
    void Start()
    {
        theList = GetControlList();
        controls.SetActive(control);
        level = PlayerPrefs.GetInt("LevelProgress");
        if (level <= 1)
            continueButt.interactable = false;
        else if (level > 1)
            continueButt.interactable = true;
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
