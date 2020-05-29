using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] Collider2D coll = null;
    [SerializeField] Animator anim = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        coll.enabled = false;
        anim.SetBool("hidden", true);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        coll.enabled = true;
        anim.SetBool("hidden", false);
    }
}
