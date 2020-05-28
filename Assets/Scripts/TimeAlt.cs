﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAlt : MonoBehaviour
{
    [SerializeField] float timeMultiplier = 2f;
    [SerializeField] float timeLength = 1f;

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

            charMovement.OutPortal(timeLength);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.OutPortal(timeLength);
        }
        else if (collision.GetComponent<Elevator>())
        {
            collision.GetComponent<Elevator>().ResetWaitMod();
        }
    }
}
