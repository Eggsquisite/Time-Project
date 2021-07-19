using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civvie : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject tombstone = null;
    [SerializeField] SpriteRenderer sprite = null;
    [SerializeField] Collider2D coll = null;
    [SerializeField] AudioClip deathSound = null;

    [Header("Civvie Stats")]
    [SerializeField] int health = 1;
    [SerializeField] float maxHurtTime = 2f;
    [SerializeField] float timeInterval = 0.1f;

    private CivManager civManager;
    private CivMovement civMovement;
    private Transform t;
    private AudioSource audioSource;
    private bool enduring;
    private float baseMaxTime, baseTimeInterval;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
        audioSource = Camera.main.GetComponent<AudioSource>();
        civManager = FindObjectOfType<CivManager>();
        civMovement = GetComponent<CivMovement>();
        baseMaxTime = maxHurtTime;
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
        //var tmpMaxTime = maxHurtTime;
        //var tmpTimeInterval = timeInterval;

        if (maxHurtTime > 0)
        {
            maxHurtTime -= Time.deltaTime;

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
        else if (maxHurtTime <= 0)
        {
            coll.enabled = true;
            sprite.enabled = true;
            enduring = false;

            maxHurtTime = baseMaxTime;
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
        audioSource.PlayOneShot(deathSound);

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
