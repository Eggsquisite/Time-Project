using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAlt : MonoBehaviour
{
    [SerializeField] float timeMultiplier = 2f;
    [SerializeField] float portalLength = 1f;
    [SerializeField] AudioClip portalSound = null;

    public AudioClip GetPortalSound()
    {
        return portalSound;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.GetComponent<CivMovement>();

            charMovement.InPortal(timeMultiplier);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.InPortal(timeMultiplier);
        }
        else if (collision.GetComponent<Elevator>())
        {
            collision.GetComponent<Elevator>().SetWaitMod(timeMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.GetComponent<CivMovement>();

            charMovement.OutPortal(portalLength);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.OutPortal(portalLength);
        }
        else if (collision.GetComponent<Elevator>())
        {
            collision.GetComponent<Elevator>().ResetWaitMod();
        }
    }
}
