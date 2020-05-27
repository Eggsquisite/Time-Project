using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civvie : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject tombstone;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Collider2D coll;

    [Header("Civvie Stats")]
    [SerializeField] int health = 1;
    [SerializeField] float maxTime = 2f;
    [SerializeField] float timeInterval = 0.1f;

    private CivManager civManager;
    private Transform t;
    private bool enduring;
    private float baseMaxTime, baseTimeInterval;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
        civManager = FindObjectOfType<CivManager>();
        baseMaxTime = maxTime;
        baseTimeInterval = timeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (enduring)
            Endurance();
    }

    private void Endurance()
    {
        //var tmpMaxTime = maxTime;
        //var tmpTimeInterval = timeInterval;

        if (maxTime > 0)
        {
            maxTime -= Time.deltaTime;

            if (timeInterval > 0)
            {
                timeInterval -= Time.deltaTime;
            }
            else if (timeInterval <= 0)
            {
                sprite.enabled = !sprite.enabled;
                timeInterval = baseTimeInterval;
            }
        }
        else if (maxTime <= 0)
        {
            coll.enabled = true;
            sprite.enabled = true;
            enduring = false;

            maxTime = baseMaxTime;
            timeInterval = baseTimeInterval;
        }
    }

    private void Fail()
    {
        // move camera to dead civvie

        civManager.Failure();
    }

    private void Dead()
    {
        // Play sound

        Fail();
        Instantiate(tombstone, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public void Hurt(int dmg)
    {
        health -= dmg;

        if (health <= 0)
            Dead();
        else
        {
            // Turn off main collider
            coll.enabled = false;
            enduring = true;
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
