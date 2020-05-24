using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAlt : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 2f;
    [SerializeField] float timeLength = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            var charMovement = collision.GetComponent<CharMovement>();

            charMovement.InPortal(speedMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            var charMovement = collision.GetComponent<CharMovement>();

            charMovement.OutPortal(timeLength);
        }
    }
}
