using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator anim;
    [SerializeField] Collider2D attackTrig;

    //[Header("Enemy Stats")]

    [Header("Movement Stats")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float walkTime = 2f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float attackTime = 1f;
    [SerializeField] bool left;

    [Header("Skree Path")]
    [SerializeField] FlightPattern fp = null;

    private float waitModifier = 1f;
    private bool timeAlt, moving, attack;
    private bool skree, flying;
    private float baseMoveSpeed, baseWaitTime, baseWalkTime, baseAttackTime, maxTimeAlt;

    // Start is called before the first frame update
    void Start()
    {
        SetAttackTrigger(0);
        SetBase();

        if (name == "Skree")
        {
            skree = true;
            fp = this.GetComponent<FlightPattern>();
        }
    }

    // Update is called once per frame
    void Update()
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
            Resetting();
    }

    private void SetBase()
    {
        baseMoveSpeed = moveSpeed;
        baseWaitTime = waitTime;
        baseWalkTime = walkTime;
        baseAttackTime = attackTime;
    }

    private void Movement()
    {
        if (!left && !attack)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else if (left && !attack)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
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

    /*
    private void Flying()
    {
        if (walkTime > 0 && walkTime >= (baseWalkTime / 2))
        {
            walkTime -= Time.deltaTime * waitModifier;

            if (!startFlight)
                StartFlight();
        }
        else if (walkTime > 0 && walkTime < (baseWalkTime / 2))
        {
            walkTime -= Time.deltaTime * waitModifier;

            if (!halfWay)
                FlightHalfWay();
        }
        else if (walkTime <= 0)
        {
            walkTime = baseWalkTime;
            moving = false;
            anim.SetBool("moving", false);

            halfWay = false;
            startFlight = false;
            rb.gravityScale = waitGrav * waitModifier;
        }
    }

    private void FlightHalfWay()
    {
        halfWay = true;
        rb.gravityScale = airGrav * waitModifier;
    }

    private void StartFlight()
    {
        startFlight = true;
        rb.gravityScale = groundGrav * waitModifier;
    }
    */

    private void Waiting()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime * waitModifier;
        else if (waitTime <= 0)
        {
            waitTime = baseWaitTime;
            left = !left;
            anim.SetBool("moving", true);
            moving = true;
        }
    }

    private void Resetting()
    {
        if (maxTimeAlt > 0)
            maxTimeAlt -= Time.deltaTime;
        else if (maxTimeAlt <= 0)
            ResetMoveSpeed();
    }

    private void ResetMoveSpeed()
    {
        maxTimeAlt = 0;
        timeAlt = false;

        waitModifier = 1f;
        if (fp != null)
            fp.WaitMod(waitModifier);
        moveSpeed = baseMoveSpeed;
        anim.SetFloat("animMultiplier", 1f);
    }

    public void OutPortal(float timeLength)
    {
        maxTimeAlt = timeLength;
        timeAlt = true;
    }

    public void InPortal(float spdMultiplier)
    {
        if (moveSpeed != baseMoveSpeed)
            moveSpeed = baseMoveSpeed;

        moveSpeed *= spdMultiplier;
        waitModifier = spdMultiplier;
        if (fp != null)
            fp.WaitMod(waitModifier);
        anim.SetFloat("animMultiplier", spdMultiplier);
    }

    public void SetAttackTrigger(float status)
    {
        if (status == 1)
            attackTrig.enabled = true;
        else
            attackTrig.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian" && attack == false)
        {
            attack = true;
            moving = false;
            walkTime = baseWalkTime;
            anim.SetTrigger("attack");
            anim.SetBool("moving", false);
        }
    }
}
