using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] Text lvlOver, loadScene;
    [SerializeField] Image uiBackground;
    [SerializeField] CivManager civs;

    private int numOfCivs;
    private float rescued = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        numOfCivs = civs.GetNumOfCivs();

        EnableText(false);
    }

    private void SuccessText()
    {
        EnableText(true);
        lvlOver.text = "All Townsfolk Alive!";
        loadScene.text = "Next Level";
    }

    public void FailureText()
    {
        EnableText(true);
        lvlOver.text = "Townsfolk died!";
        loadScene.text = "Retry";
    }

    private void EnableText(bool status)
    {
        uiBackground.enabled = status;
        lvlOver.enabled = status;
        loadScene.enabled = status;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            rescued++;

        if (rescued == numOfCivs)
            SuccessText();
    }

}
