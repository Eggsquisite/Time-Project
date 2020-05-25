using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] Text lvlOver, nextScene, retryScene;
    [SerializeField] Image uiBackground;
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
        EnableText(true, 1);
        lvlOver.text = "All Townsfolk Alive!";
        nextScene.text = "Next Level";
    }

    public void FailureText()
    {
        EnableText(true, 0);
        lvlOver.text = "Townsfolk died!";
        retryScene.text = "Retry";
    }

    private void EnableText(bool status, int success)
    {
        uiBackground.enabled = status;
        lvlOver.enabled = status;

        if (success == -1)
        {
            nextScene.enabled = status;
            retryScene.enabled = status;
        }
        else if (success == 0)
            retryScene.enabled = status;
        else if (success == 1)
            nextScene.enabled = status;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            rescued++;

        if (rescued == numOfCivs)
            SuccessText();
    }

}
