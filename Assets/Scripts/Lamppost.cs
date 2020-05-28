using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamppost : MonoBehaviour
{

    [SerializeField] LevelEnd lvlEnd = null;
    [SerializeField] CivManager civs = null;

    private int numOfCivs;
    private float rescued = 0;

    // Start is called before the first frame update
    void Start()
    {
        numOfCivs = civs.GetNumOfCivs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            rescued++;

        if (rescued == numOfCivs)
            lvlEnd.SuccessText();
    }
}
