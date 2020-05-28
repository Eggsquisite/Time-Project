using System.Collections;
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
    private float maxTimeAlt, maxGravAlt;
    private bool timeAlt, gravAlt;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //anim.SetBool("moving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitModifier > 0)
        t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime * waitModifier, t.position.y);

        if (timeAlt)
            ResetTime();

        if (gravAlt)
            ResetGrav();
    }


    private void ResetTime()
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

        rb.gravityScale = 1f;
        waitModifier = 1f;
        anim.SetFloat("animMultiplier", 1f);
    }

    public void OutPortal(float timeLength)
    {
        maxTimeAlt = timeLength;
        timeAlt = true;
    }

    public void InPortal(float spdMultiplier)
    {
        if (waitModifier != spdMultiplier)
            waitModifier = spdMultiplier;

        anim.SetFloat("animMultiplier", spdMultiplier);
    }

    public void GravPortal(float grav, float gravLength)
    {
        rb.gravityScale = grav;

        maxGravAlt = gravLength;
        gravAlt = true;
        feet.enabled = false;
    }

    private void ResetGrav()
    {
        if (maxGravAlt > 0)
            maxGravAlt -= Time.deltaTime;
        else if (maxGravAlt <= 0)
        {
            gravAlt = false;
            rb.gravityScale = 1f;
            feet.enabled = true;
        }
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
