using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAlt : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 2f;
    [SerializeField] float timeLength = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.GetComponent<CivMovement>();

            charMovement.InPortal(speedMultiplier);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.InPortal(speedMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.GetComponent<CivMovement>();

            charMovement.OutPortal(timeLength);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            // Enemy doesn't stay sped up as long as civilians do
            enemyMovement.OutPortal(timeLength / 2);
        }
    }
}
