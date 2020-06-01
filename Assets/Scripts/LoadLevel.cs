using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] StartMenu startMenu = null;
    [SerializeField] GameObject lvlList = null;

    private List<Transform> list;

    private void Start()
    {
        list = GetNumOfLvls();
        LevelCheck();
    }

    private void LevelCheck()
    {
        int tmp;
        for (int i = 0; i < list.Count; i++)
        {
            tmp = Int32.Parse(list[i].gameObject.name);
            
            if (tmp <= PlayerPrefs.GetInt("LevelProgress"))
            {
                list[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    private List<Transform> GetNumOfLvls()
    {
        var tmp = new List<Transform>();

        foreach (Transform lvl in lvlList.transform)
        {
            tmp.Add(lvl);
        }

        return tmp;
    }

    public void SetLevel(int lvl)
    {
        startMenu.LoadLevel(lvl);
    }
}
