using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamppost : MonoBehaviour
{

    [SerializeField] Animator lightAnim = null;
    [SerializeField] GameObject magicWall = null;

    private CivManager civs;
    private int numOfCivs;
    private float rescued = 0;
    private bool success;

    // Start is called before the first frame update
    void Awake()
    {
        civs = FindObjectOfType<CivManager>();
        
        if (magicWall != null)
            magicWall.SetActive(false);

        if (civs != null)
        {
            numOfCivs = civs.GetNumOfCivs();
        }
    }

    private void Success()
    {
        if (civs == null)
        {
            civs = FindObjectOfType<CivManager>();
        }

        lightAnim.SetBool("lvlSuccess", true);
        civs.Success();

        if (magicWall != null)
            magicWall.SetActive(true);
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civillian")
            rescued++;

        if (rescued == numOfCivs && !success)
        {
            success = true;
            Success();
        }
    }
}
