using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civvie : MonoBehaviour
{
    [SerializeField] int health = 1;

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
        
    }

    private void Ed()
    { 
        
    }

    public void Hurt(int dmg)
    {
        health -= dmg;

        if (health <= 0)
            Dead();
        else
            Ed();
    }
}
