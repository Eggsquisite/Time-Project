using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamppost : MonoBehaviour
{

    [SerializeField] LevelEnd lvlEnd = null;
    [SerializeField] CivManager civs = null;
    [SerializeField] Animator lightAnim = null;

    private int numOfCivs;
    private float rescued = 0;

    // Start is called before the first frame update
    void Start()
    {
        numOfCivs = civs.GetNumOfCivs();
    }

    private void Success()
    {
        lightAnim.SetBool("lvlSuccess", true);
        lvlEnd.SuccessText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            rescued++;

        if (rescued == numOfCivs)
            Success();
    }
}
