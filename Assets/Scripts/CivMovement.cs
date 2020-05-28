using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float waitTime = 2f;

    private Transform t;
    private Animator anim;
    private float waitModifier = 1f;
    private float maxTimeAlt;
    private bool timeAlt;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        //anim.SetBool("moving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitModifier > 0)
        t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime * waitModifier, t.position.y);

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
