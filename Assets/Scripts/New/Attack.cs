using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
            collision.GetComponent<Civvie>().Hurt(dmg);
    }
}
