using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : MonoBehaviour
{
    GameObject itemPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        itemPosition = this.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("Stealing...");
            collision.transform.position = itemPosition.transform.position;
            Destroy(itemPosition.gameObject);
            collision.transform.parent = this.gameObject.transform;

            GetComponent<Character>().Thievery(true);
        }
    }
}
