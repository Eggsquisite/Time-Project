using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject uiBackground, lvlOver, retryLvl, nextLvl;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        EnableText(false, -1);
    }

    public void SuccessText()
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
}
