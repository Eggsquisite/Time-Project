using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Collider2D feet = null;
    [SerializeField] private bool ghostObserving;

    private Transform t;
    private Animator anim;
    private Rigidbody2D rb;
    private float waitModifier = 1f;
    private float maxTimeAlt, maxGravAlt, baseGrav, baseMoveSpeed, newGrav, restoreMult;
    private bool timeAlt, gravAlt, restoreTime, stopMovement, isBeingAttacked;
    private bool observeMode = true;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("moving", true);
        baseGrav = rb.gravityScale;
        baseMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitModifier > 0 && !observeMode && !stopMovement && !ghostObserving && !isBeingAttacked)
            t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime * waitModifier, t.position.y);
        else if (ghostObserving)
            t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime * waitModifier, t.position.y);

        if (timeAlt)
            TimeWait();
        else if (restoreTime)
            RestoreTime();

        if (gravAlt)
            GravWait();
        //else if (restoreGrav)
            //RestoreGrav();
    }

    public void ObserveFinished() {
        if (ghostObserving)
            Destroy(gameObject);

        observeMode = false;
    }

    public void IsAttacked() {
        isBeingAttacked = true;
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

        // For speedup
        if (waitModifier > 1)
        {
            waitModifier -= Time.deltaTime * restoreMult;

            if (waitModifier <= 1)
            {
                //if (gravAlt)
                //rb.gravityScale = baseGrav;

                waitModifier = 1f;
                restoreTime = false;
            }
        }
        // For slowdown
        else if (waitModifier < 1)
        {
            waitModifier += Time.deltaTime / 5;

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

    public void TimePortal(float spdMultiplier, float timeLength, float restore)
    {
        if (waitModifier != spdMultiplier)
        {
            waitModifier = spdMultiplier;
            restoreMult = ((moveSpeed * waitModifier) - moveSpeed) / restore;

        }

        //if (gravAlt)
        //else if (!gravAlt)
            //rb.gravityScale *= waitModifier;
        
        if (waitModifier > 0)
            rb.gravityScale *= waitModifier;
        timeAlt = true;
        maxTimeAlt = timeLength;
        anim.SetFloat("animMultiplier", spdMultiplier);
    }

    public void StopMovement() {
        // called for willy
        stopMovement = true;
        anim.SetBool("moving", false);
        anim.SetTrigger("prepareRun");
    }

    public void ResumeMovement() {
        // called for willy
        stopMovement = false;
        TimePortal(3f, 0.5f, 5f);
        anim.SetBool("moving", true);
        anim.ResetTrigger("prepareRun");
    }

    public void GravPortal(float grav, float gravLength)
    {
        rb.gravityScale = grav * waitModifier;

        newGrav = grav;
        gravAlt = true;
        feet.enabled = false;
        maxGravAlt = gravLength;
    }

    public void RoseHide()
    {
        moveSpeed = 0f;
    }

    public void RoseReveal()
    {
        moveSpeed = baseMoveSpeed;
    }

    public float GetWaitModifier()
    {
        return waitModifier;
    }
}
