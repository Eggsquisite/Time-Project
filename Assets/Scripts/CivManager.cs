using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivManager : MonoBehaviour
{

    [SerializeField] LevelEnd lvlEnd;
    [SerializeField] Transform civs;
    [SerializeField] float deathTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Failure()
    {
        StartCoroutine(Failed());
    }

    private IEnumerator Failed()
    { 
        yield return new WaitForSeconds(deathTime);
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
