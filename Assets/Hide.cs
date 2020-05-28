using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] Collider2D coll;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Hiding" + collision.name);
        coll.enabled = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Not Hiding");
        coll.enabled = true;
        
    }
}
