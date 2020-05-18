using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : MonoBehaviour
{
    GameObject childItem = null;

    // Start is called before the first frame update
    void Start()
    {
        //childItem = this.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("Stealing...");
            //collision.transform.position = childItem.transform.position;
            collision.transform.position = Vector2.MoveTowards(collision.transform.position, this.gameObject.transform.position, 2 * Time.deltaTime);
            collision.transform.parent = this.gameObject.transform;

            GetComponent<Character>().Thievery(true);
        }
    }
}
