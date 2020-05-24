using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 1.25f;
    [SerializeField] float animMultiplier = 1.5f;
    [SerializeField] float waitMultiplier = 2f;

    Collider2D coll;
    Color lightBlue = new Color(0, 1, 1);
    float zRotateSpeed = -25f;
    bool ready = false;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }

    void Update()
    {
        if (!ready)
            transform.Rotate(0, 0, zRotateSpeed * Time.deltaTime);
        else
            transform.Rotate(0, 0, zRotateSpeed * 10 * Time.deltaTime);
    }

    public void SetReady(bool readyStatus)
    {
        if (ready != readyStatus)
            ready = readyStatus;

        // Turn on collision
        if (ready)
        {
            coll.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            // Speed up movespeed and animation
            SpeedingUp(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character") 
        {
            // Resets move/anim speed
            Stabilizing(collision);
        }
    }

    private void SpeedingUp(Collider2D collision)
    {
        var characterPath = collision.GetComponent<CharacterPathing>();
        var characterAnim = collision.GetComponent<Animator>();

        //characterPath.SetWaitTime(waitMultiplier);
        //characterPath.AltTimeAffect(speedMultiplier, lightBlue);
        characterAnim.SetFloat("runMultiplier", animMultiplier);
    }

    private void Stabilizing(Collider2D collision)
    {
        var characterPath = collision.GetComponent<CharacterPathing>();
        var characterAnim = collision.GetComponent<Animator>();

        characterPath.SetWaitTime(1);
        characterPath.ResetTime();
        characterAnim.SetFloat("runMultiplier", 1);
    }
}
