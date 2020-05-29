using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int dmg = 1;
    [SerializeField] GameObject itMe = null;

    Animator anim;
    EnemyMovement enemy;

    private void Start()
    {
        anim = itMe.GetComponent<Animator>();
        enemy = itMe.GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var civ = collision.GetComponent<Civvie>();

            if (civ.name.Contains("Ed") && civ.GetHealth() > 1)
            {
                anim.SetTrigger("hit");
                enemy.Dead();
            }

            civ.Hurt(dmg);
        }
    }
}
