﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float waitTime = 2f;
    [SerializeField] Collider2D feet = null;

    private Transform t;
    private Animator anim;
    private Rigidbody2D rb;
    private float waitModifier = 1f;
    private float maxTimeAlt, maxGravAlt, baseGrav;
    private bool timeAlt, gravAlt, restoreTime, restoreGrav;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        baseGrav = rb.gravityScale;
        //anim.SetBool("moving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitModifier > 0)
        t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime * waitModifier, t.position.y);

        if (timeAlt)
            TimeWait();
        else if (restoreTime)
            RestoreTime();

        if (gravAlt)
            GravWait();
        else if (restoreGrav)
            RestoreGrav();
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
            waitModifier -= Time.deltaTime;
        else if (waitModifier <= 1)
        { 
            waitModifier = 1f;
            rb.gravityScale = baseGrav;
            restoreTime = false;
        }
    }

    private void GravWait()
    {
        if (maxGravAlt > 0)
            maxGravAlt -= Time.deltaTime;
        else if (maxGravAlt <= 0)
        {
            maxGravAlt = 0;
            gravAlt = false;
            //restoreGrav = true;
            feet.enabled = true;
            rb.gravityScale = baseGrav;
        }
    }

    private void RestoreGrav()
    {
        /*if (rb.gravityScale < baseGrav)
            rb.gravityScale += Time.deltaTime;
        else if (rb.gravityScale >= baseGrav)
        {
            rb.gravityScale = baseGrav;
            restoreGrav = false;
        }*/
    }

    public void TimePortal(float spdMultiplier, float timeLength)
    {
        if (waitModifier != spdMultiplier)
            waitModifier = spdMultiplier;
        
        timeAlt = true;
        maxTimeAlt = timeLength;
        anim.SetFloat("animMultiplier", spdMultiplier);
    }

    public void GravPortal(float grav, float gravLength)
    {
        rb.gravityScale = grav;

        maxGravAlt = gravLength;
        gravAlt = true;
        feet.enabled = false;
    }


    public void OnElevator()
    {
        waitModifier = 0f;
        anim.SetBool("moving", false);
    }

    public void OffElevator()
    {
        waitModifier = 1f;
        anim.SetBool("moving", true);
    }
}
