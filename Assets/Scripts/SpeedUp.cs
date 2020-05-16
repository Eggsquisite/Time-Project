using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 1.25f;
    [SerializeField] float animMultiplier = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            collision.GetComponent<CharacterPathing>().NewMoveSpeed(speedMultiplier);
            collision.GetComponent<Animator>().SetFloat("runMultiplier", animMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character") 
        {
            collision.GetComponent<CharacterPathing>().ResetMoveSpeed();
            collision.GetComponent<Animator>().SetFloat("runMultiplier", 1);
        }
    }
}
