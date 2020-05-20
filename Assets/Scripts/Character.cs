using System.Collections;
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
    }

    public bool GetThiefStatus()
    {
        return thief;
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
        thiefPath.AltTimeAffect(0, Color.yellow);
        charPath.AltTimeAffect(0, Color.yellow);

        yield return new WaitForSeconds(waitKnightTime);

        // Reset movement of knight and thief
        thiefPath.ResetTime();
        charPath.ResetTime();

        // Destroy stolen item 
        if(thief.transform.GetChild(1) != null && thiefCharacter.GetThiefStatus() == true)
            Destroy(thief.transform.GetChild(1).gameObject);
        
        thiefCharacter.Thievery(false);
    }
}
