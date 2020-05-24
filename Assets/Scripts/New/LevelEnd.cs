using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] Text lvlOver, loadScene;
    [SerializeField] GameObject civs;

    private List<Transform> numOfCivs;
    private float rescued = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        numOfCivs = GetNumOfCivs();

        EnableText(false);
    }

    private void SuccessText()
    {
        EnableText(true);
        lvlOver.text = "All Townsfolk Saved!";
        loadScene.text = "Next Level";
    }

    private void EnableText(bool status)
    {
        lvlOver.enabled = status;
        loadScene.enabled = status;
    }

    private List<Transform> GetNumOfCivs()
    {
        var tmp = new List<Transform>();

        foreach (Transform civ in civs.transform)
        {
            tmp.Add(civ);
        }

        return tmp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            rescued++;

        if (rescued == numOfCivs.Count)
            SuccessText();
    }

}
