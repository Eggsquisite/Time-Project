using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
            EnemyAttack();
    }

    private void EnemyAttack()
    {
        Debug.Log("Attacking");
        anim.SetTrigger("Attack");
    }
}
