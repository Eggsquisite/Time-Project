using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltTime : MonoBehaviour
{
    [SerializeField] float timeMultiplier = 2f;
    [SerializeField] float timeLength = 1f;
    [SerializeField] AudioClip portalSound = null;

    public AudioClip GetPortalSound()
    {
        return portalSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.GetComponent<CivMovement>();

            charMovement.TimePortal(timeMultiplier, timeLength);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.TimePortal(timeMultiplier, timeLength);
        }
        else if (collision.GetComponent<Elevator>())
        {
            collision.GetComponent<Elevator>().SetWaitMod(timeMultiplier);
        }
    }
}
