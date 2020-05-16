using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 1.25f;
    [SerializeField] float animMultiplier = 1.5f;

    float oldSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

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
            collision.GetComponent<CharacterPathing>().OldMoveSpeed(speedMultiplier);
            collision.GetComponent<Animator>().SetFloat("runMultiplier", 1);
        }
    }
}
