using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float waitKnightTime = 2f;
    private bool knight = false;
    private bool stolen = false;
    private bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Contains("Knight"))
            knight = true;
    }

    public void Thievery(bool thief)
    {
        stolen = thief;
        Debug.Log("Has stolen");
    }

    public bool GetThiefStatus()
    {
        return stolen;
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
        if (collision.tag == "Character" && collision.GetComponent<Character>().GetThiefStatus() == true && knight == true)
        {
            Debug.Log("Delivering Justice");
            StartCoroutine(KnightJustice(collision.gameObject));
        }
    }

    IEnumerator KnightJustice(GameObject thief)
    {
        var thiefCharacter = GetComponent<Character>();

        thief.GetComponent<CharacterPathing>().AltMoveSpeed(0);
        this.GetComponent<CharacterPathing>().AltMoveSpeed(0);

        // Stopped status for speed up purposes
        thiefCharacter.SetStopped(true);
        Debug.Log("Stopped " + thiefCharacter.GetStoppedStatus());
        this.SetStopped(true);

        yield return new WaitForSeconds(waitKnightTime);

        thiefCharacter.Thievery(false);
        thiefCharacter.SetStopped(false);
        this.SetStopped(false);
        Debug.Log("Unstopped " + thiefCharacter.GetStoppedStatus());

        thief.GetComponent<CharacterPathing>().ResetMoveSpeed();
        this.GetComponent<CharacterPathing>().ResetMoveSpeed();
        Destroy(thief.transform.GetChild(0).gameObject);

    }
}
