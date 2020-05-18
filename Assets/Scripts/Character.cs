﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float waitKnightTime = 2f;
    [SerializeField] bool thief = false;

    CharacterPathing charPath = null;
    private bool knight = false;
    private bool arresting = false;
    //private bool thief = false;
    private bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        charPath = GetComponent<CharacterPathing>();
        if (this.name.Contains("Knight"))
            knight = true;
    }

    public void Thievery(bool status)
    {
        thief = status;
        Debug.Log("Has stolen");
    }

    public bool GetThiefStatus()
    {
        return thief;
    }

    public void SetStopped(bool status)
    {
        stopped = status;
    }

    public bool GetStoppedStatus()
    {
        return stopped;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" && collision.GetComponent<Character>().GetThiefStatus() == true && knight == true && arresting == false)
        {
            Debug.Log("Delivering Justice");
            arresting = true;
            StartCoroutine(KnightJustice(collision.gameObject));
        }
    }

    IEnumerator KnightJustice(GameObject thief)
    {
        var thiefCharacter = thief.GetComponent<Character>();
        var thiefPath = thief.GetComponent<CharacterPathing>();

        // Stop knight and thief for confrontation
        thiefPath.AltMoveSpeed(0);
        charPath.AltMoveSpeed(0);

        yield return new WaitForSeconds(waitKnightTime);

        // Reset movement of knight and thief
        thiefPath.ResetMoveSpeed();
        charPath.ResetMoveSpeed();

        // Destroy stolen item 
        if(thief.transform.GetChild(0) != null && thiefCharacter.GetThiefStatus() == true)
            Destroy(thief.transform.GetChild(0).gameObject);
        
        thiefCharacter.Thievery(false);
    }
}
