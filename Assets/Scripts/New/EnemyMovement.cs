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

    [Header("Skree Stats")]
    [SerializeField] float groundTime = 3f;
    [SerializeField] float yMax = 5f;
    [SerializeField] float yMin = 0f;

    private Rigidbody2D rb;
    private float waitModifier = 1f;
    private bool timeAlt, moving, attack, skree;
    private float baseMoveSpeed, baseWaitTime, baseWalkTime, baseGroundTime, baseAttackTime, maxTimeAlt;

    // Start is called before the first frame update
    void Start()
    {
        SetAttackTrigger(0);

        rb = GetComponent<Rigidbody2D>();
        SetBase();

        if (name == "Skree")
        {
            skree = true;
            rb.gravityScale = 0f;
        }
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

        if (skree)
        {
            float y = Mathf.Clamp(transform.position.y, yMin, yMax);
            transform.position = new Vector2(transform.position.x, y);
        }
    }

    private void SetBase()
    {
        baseMoveSpeed = moveSpeed;
        baseWaitTime = waitTime;
        baseGroundTime = groundTime;
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

        if (skree)
            Flying();
        else
        {
            if (walkTime > 0)
                walkTime -= Time.deltaTime * waitModifier;
            else if (walkTime <= 0)
            {
                walkTime = baseWalkTime;
                anim.SetBool("moving", false);
                moving = false;
            }
        }
    }

    private void Flying()
    {
        if (walkTime > 0 && walkTime >= (baseWalkTime / 2))
        {
            walkTime -= Time.deltaTime * waitModifier;
            rb.gravityScale = 0.1f;
        }
        else if (walkTime > 0 && walkTime < (baseWalkTime / 2))
        {
            walkTime -= Time.deltaTime * waitModifier;
            rb.gravityScale = -0.25f;
            Debug.Log("halfway");
        }
        else if (walkTime <= 0)
        {
            walkTime = baseWalkTime;
            Debug.Log("Zero grav");
            anim.SetBool("moving", false);
            moving = false;
            rb.gravityScale = 0.3f;
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
        Debug.Log("Setting attack trig" + status);

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
