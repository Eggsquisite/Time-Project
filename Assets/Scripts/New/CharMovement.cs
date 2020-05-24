using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    private Transform t;
    private Animator anim;
    private float baseMoveSpeed, maxTimeAlt;
    private bool timeAlt = false;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        baseMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime, t.position.y);

        if (timeAlt)
            Resetting();
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

        moveSpeed = baseMoveSpeed;
        anim.SetFloat("runMultiplier", 1f);
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
        anim.SetFloat("runMultiplier", spdMultiplier);
        //timeAlt = true;
    }
}
