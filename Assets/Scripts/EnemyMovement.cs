using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator anim = null;
    [SerializeField] Collider2D attackTrig = null;
    [SerializeField] AudioClip attackSound = null;
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] Collider2D feet = null;

    //[Header("Enemy Stats")]

    [Header("Movement Stats")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float walkTime = 2f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float attackTime = 1f;
    [SerializeField] bool right;

    [Header("Skelly Stats")]
    [SerializeField] float rezTime = 2f;

    [Header("Skree Path")]
    [SerializeField] FlightPattern fp = null;

    private Collider2D coll;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private float waitModifier = 1f;
    private bool timeAlt, gravAlt, restoreTime, moving, attack, death;
    private bool skelly, gobby;
    private bool skree, flying;
    private float baseWaitTime, baseWalkTime, baseAttackTime, baseRezTime, baseGrav; 
    private float maxTimeAlt, maxGravAlt, newGrav;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        SetAttackTrigger(0);
        SetBase();

        if (name.Contains("Skree"))
        {
            skree = true;
            fp = this.GetComponent<FlightPattern>();
        }
        else if (name.Contains("Skelly"))
        {
            skelly = true;
        }
        else if (name.Contains("Gobby"))
            gobby = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!death)
        {
            if (skree && !flying)
            {
                fp.Fly();
                flying = true;
            }
            else if (moving && !attack && !flying)
                Movement();
            else if (!moving && !attack && !flying)
                Waiting();
            else if (attack)
            {
                if (attackTime > 0)
                    attackTime -= Time.deltaTime;
                else if (attackTime <= 0)
                {
                    attackTime = baseAttackTime;
                    attack = false;
                }
            }

            if (timeAlt)
                TimeWait();
            else if (restoreTime)
                RestoreTime();

            if (gravAlt)
                GravWait();
        }

        if (death && skelly)
        {
            if (rezTime > 0)
                rezTime -= Time.deltaTime;
            else if (rezTime <= 0)
                anim.SetBool("rez", true);
        }
    }

    private void SetBase()
    {
        baseWaitTime = waitTime;
        baseWalkTime = walkTime;
        baseAttackTime = attackTime;
        baseRezTime = rezTime;
        baseGrav = rb.gravityScale;
    }

    private void Movement()
    {
        if (!right && !attack)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * waitModifier, transform.position.y);
        }
        else if (right && !attack)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime * waitModifier, transform.position.y);
        }

        if (walkTime > 0)
            walkTime -= Time.deltaTime * waitModifier;
        else if (walkTime <= 0)
        {
            walkTime = baseWalkTime;
            anim.SetBool("moving", false);
            moving = false;
        }
    }

    private void Waiting()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime * waitModifier;
        else if (waitTime <= 0)
        {
            if (gobby)
                waitTime = 0;
            else
            {
                right = !right;
                waitTime = baseWaitTime;
            }

            moving = true;
            anim.SetBool("moving", true);
        }
    }

    private void TimeWait()
    {
        if (maxTimeAlt > 0)
            maxTimeAlt -= Time.deltaTime;
        else if (maxTimeAlt <= 0)
        {
            maxTimeAlt = 0;
            timeAlt = false;
            restoreTime = true;
        }
    }

    private void RestoreTime()
    {
        anim.SetFloat("animMultiplier", waitModifier);

        if (waitModifier > 1)
        {
            waitModifier -= Time.deltaTime;
            if (fp != null)
                fp.WaitMod(waitModifier);

            if (waitModifier <= 1)
            {
                //if (gravAlt)
                //rb.gravityScale = baseGrav;

                waitModifier = 1f;
                restoreTime = false;
            }
        }
        else if (waitModifier < 1)
        {
            waitModifier += Time.deltaTime / 5;
            if (fp != null)
                fp.WaitMod(waitModifier);

            if (waitModifier >= 1)
            {
                waitModifier = 1f;
                restoreTime = false;
            }
        }
    }

    private void GravWait()
    {
        if (maxGravAlt > 0 && timeAlt)
        {
            maxGravAlt -= Time.deltaTime;
        }
        else if (maxGravAlt > 0 && !timeAlt)
        {
            maxGravAlt -= Time.deltaTime;
            rb.gravityScale = newGrav;
        }
        else if (maxGravAlt <= 0)
        {
            Debug.Log("Restoring Grav");
            maxGravAlt = 0;
            gravAlt = false;
            //restoreGrav = true;
            feet.enabled = true;
            rb.gravityScale = baseGrav;
        }
    }

    private void RestoreGrav()
    {
        /*
        if (rb.gravityScale < baseGrav)
            rb.gravityScale += Time.deltaTime;
        else if (rb.gravityScale >= baseGrav)
        {
            rb.gravityScale = baseGrav;
            restoreGrav = false;
        } */
    }

    public void TimePortal(float spdMultiplier, float timeLength)
    {
        if (waitModifier != spdMultiplier)
            waitModifier = spdMultiplier;

        rb.gravityScale *= waitModifier;

        timeAlt = true;
        maxTimeAlt = timeLength;
        anim.SetFloat("animMultiplier", spdMultiplier);

        if (fp != null)
            fp.WaitMod(waitModifier);
    }

    public void GravPortal(float grav, float gravLength)
    {
        if (!skree)
            rb.gravityScale = grav * waitModifier;

        newGrav = grav;
        gravAlt = true;
        feet.enabled = false;
        maxGravAlt = gravLength;
    }


    public void SetAttackTrigger(float status)
    {
        if (status == 1)
            attackTrig.enabled = true;
        else
            attackTrig.enabled = false;
    }

    public void Dead()
    {
        death = true;
        coll.enabled = false;
    }

    private void Rez()
    {
        death = false;
        coll.enabled = true;
        rezTime = baseRezTime;
        anim.SetBool("rez", false);
    }

    private void SkreeDeath()
    {
        fp.enabled = false;
        rb.gravityScale = 1f;
    }

    private void AttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    private void DeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public bool GetGobby()
    {
        return gobby;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian" && attack == false)
        {
            attack = true;
            moving = false;
            if (skree)
                fp.Attacking(attack, baseAttackTime);

            walkTime = baseWalkTime;
            anim.SetTrigger("attack");
            anim.SetBool("moving", false);
        }
        else if (collision.tag == "Enemy" && attack == false && gobby)
        {
            attack = true;
            moving = false;

            walkTime = baseWalkTime;
            anim.SetTrigger("attack");
            anim.SetBool("moving", false);
        }
    }

    public void OnElevator()
    {
        waitModifier = 0f;
        moving = false;
        anim.SetBool("moving", false);
    }

    public void OffElevator()
    {
        waitModifier = 1f;
        moving = true;
        anim.SetBool("moving", true);
    }
}
