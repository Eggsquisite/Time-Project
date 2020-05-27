using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject uiBackground, lvlOver, nextLvl, retryLvl;
    [SerializeField] CivManager civs;

    private int numOfCivs;
    private float rescued = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        numOfCivs = civs.GetNumOfCivs();
        EnableText(false, -1);
    }

    private void SuccessText()
    {
        lvlOver.GetComponent<Text>().text = "All Townsfolk Alive!";
        EnableText(true, 1);
    }

    public void FailureText()
    {
        lvlOver.GetComponent<Text>().text = "Townsfolk died!";
        EnableText(true, 0);
    }

    private void EnableText(bool status, int success)
    {
        uiBackground.SetActive(status);
        lvlOver.SetActive(status);

        if (success == 0)
            retryLvl.SetActive(status);
        else if (success == 1)
            nextLvl.SetActive(status);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            rescued++;

        if (rescued == numOfCivs)
            SuccessText();
    }

}
