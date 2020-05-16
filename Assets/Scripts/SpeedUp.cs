﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 1.25f;
    [SerializeField] float animMultiplier = 1.5f;
    [SerializeField] float waitMultiplier = 0.5f;

    float zRotateSpeed = -25f;
    Collider2D coll;
    bool ready = false;

    private void Start()
    {
        coll = GetComponent<CircleCollider2D>();
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
            coll.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            var character = collision.GetComponent<CharacterPathing>();
            var characterAnim = collision.GetComponent<Animator>();

            // Increases move/anim speed and decreases wait time by multiplier
            character.NewMoveSpeed(speedMultiplier);
            var tmpWait = character.GetWaitTime();
            character.SetWaitTime(tmpWait * waitMultiplier);
            characterAnim.SetFloat("runMultiplier", animMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character") 
        {
            var character = collision.GetComponent<CharacterPathing>();
            var characterAnim = collision.GetComponent<Animator>();

            // Resets move/anim speed
            character.ResetMoveSpeed();
            characterAnim.SetFloat("runMultiplier", 1);
        }
    }
}
