using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float walkTime = 2f;
    [SerializeField] float waitTime = 1f;

    private Transform t;
    private Animator anim;
    private float waitModifier = 1f;
    private float baseMoveSpeed, baseWaitTime, baseWalkTime, maxTimeAlt;
    private bool timeAlt, moving, left;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        baseMoveSpeed = moveSpeed;
        baseWaitTime = waitTime;
        baseWalkTime = walkTime;
        left = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            Movement();
        else
            Waiting();

        if (timeAlt)
            Resetting();
    }

    private void Movement()
    {
        if (!left)
        {
            t.rotation = new Quaternion(0, 0, 0, 0);
            t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime, t.position.y);
        }
        else
        {
            t.rotation = new Quaternion(0, 180, 0, 0);
            t.position = new Vector2(t.position.x - moveSpeed * Time.deltaTime, t.position.y);
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

}
