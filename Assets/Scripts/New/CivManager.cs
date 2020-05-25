using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivManager : MonoBehaviour
{

    [SerializeField] LevelEnd lvlEnd;

    [SerializeField] Transform civs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Failure()
    {
        lvlEnd.FailureText();
    }

    public int GetNumOfCivs()
    {
        int tmp = 0;

        foreach (Transform civ in civs.transform)
            tmp++;

        return tmp;
    }
}
