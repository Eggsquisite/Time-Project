using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civvie : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] GameObject tombstone;
    [SerializeField] SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Dead()
    {
        // Play sound

        Instantiate(tombstone, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    private void Ed()
    { 
        
    }

    public void Hurt(int dmg)
    {
        health -= dmg;
        Debug.Log("Hurt");

        if (health <= 0)
            Dead();
        else
            Ed();
    }
}
