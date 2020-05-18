using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float waitKnightTime = 2f;
    private bool knight = false;
    private bool stolen = false;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Contains("Knight"))
            knight = true;
    }

    public void CommitThievery(bool thief)
    {
        stolen = thief;
        Debug.Log("Has stolen");
    }

    public bool GetThiefStatus()
    {
        return stolen;
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
        thief.GetComponent<CharacterPathing>().AltMoveSpeed(0);
        this.GetComponent<CharacterPathing>().AltMoveSpeed(0);
        yield return new WaitForSeconds(waitKnightTime);

        thief.GetComponent<CharacterPathing>().ResetMoveSpeed();
        this.GetComponent<CharacterPathing>().ResetMoveSpeed();
        Destroy(thief.transform.GetChild(0).gameObject);
    }
}
