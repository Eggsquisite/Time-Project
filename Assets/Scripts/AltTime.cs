﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltTime : MonoBehaviour
{
    [SerializeField] float timeMultiplier = 2f;
    [SerializeField] float timeLength = 1f;
    [SerializeField] float portalTimer = 2f;
    [SerializeField] AudioClip portalSound = null;

    private ParticleSystem fx;
    private Animator anim;
    private Collider2D coll;
    private AudioSource audioSource;
    private bool portalStart;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        fx = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }

    private void Update()
    {
        if (portalStart)
            PortalCountdown();
    }

    private void PortalCountdown()
    {
        if (portalTimer > 0)
            portalTimer -= Time.deltaTime;
        else if (portalTimer <= 0)
        {
            portalStart = false;
            anim.SetTrigger("close");
        }
    }

    private void StartPortalTimer()
    {
        portalStart = true;
    }

    private void EffectsOff()
    {
        fx.Stop();
        coll.enabled = false;
    }

    private void EffectsOn()
    {
        fx.Play();
        coll.enabled = true;
    }

    private void DestroyPortal()
    {
        Destroy(gameObject);
    }

    public AudioClip GetPortalSound()
    {
        return portalSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.GetComponent<CivMovement>();

            charMovement.TimePortal(timeMultiplier, timeLength);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.TimePortal(timeMultiplier, timeLength);
        }
    }
}
