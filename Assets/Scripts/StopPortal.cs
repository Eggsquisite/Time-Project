using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPortal : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float timeMultiplier = 2f;
    [SerializeField] float timeLength = 1f;
    [SerializeField] float restoreMult = 5f;
    [SerializeField] AudioClip portalSound = null;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        audioSource.PlayOneShot(portalSound);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void DestroyPortal()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.GetComponent<CivMovement>();

            charMovement.TimePortal(timeMultiplier, timeLength, restoreMult);
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.GetComponent<EnemyMovement>();

            enemyMovement.TimePortal(timeMultiplier, timeLength, restoreMult);
        }
    }
}
