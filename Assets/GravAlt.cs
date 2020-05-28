using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravAlt : MonoBehaviour
{
    [SerializeField] float portalLength = 1f;
    [SerializeField] float gravMultiplier = -1f;
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

            charMovement.GravPortal(gravMultiplier, portalLength);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.GravPortal(gravMultiplier, portalLength);
        }
    }
}
