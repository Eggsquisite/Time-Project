using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator anim;
    [SerializeField] Collider2D attackTrig;

    [Header("Enemy Stats")]
    [SerializeField] int dmg = 1;

    [Header("Movement Stats")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float walkTime = 2f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float attackTime = 1f;

    private float waitModifier = 1f;
    private bool timeAlt, moving, left, attack;
    private float baseMoveSpeed, baseWaitTime, baseWalkTime, baseAttackTime, maxTimeAlt;

    // Start is called before the first frame update
    void Start()
    {
        SetAttackTrigger(0);

        baseMoveSpeed = moveSpeed;
        baseWaitTime = waitTime;
        baseWalkTime = walkTime;
        baseAttackTime = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && !attack)
            Movement();
        else if (!moving && !attack)
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

            //anim
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
