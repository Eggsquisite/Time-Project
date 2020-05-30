using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltGrav : MonoBehaviour
{
    [SerializeField] float portalLength = 1f;
    [SerializeField] float gravMultiplier = -1f;
    [SerializeField] float portalTimer = 2f;
    [SerializeField] AudioClip portalSound = null;

    private ParticleSystem fx;
    private Collider2D coll;
    private Animator anim;
    private AudioSource audioSource;
    private bool portalStart;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        fx = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
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

            charMovement.GravPortal(gravMultiplier, portalLength);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.GravPortal(gravMultiplier, portalLength);
        }
    }
}
