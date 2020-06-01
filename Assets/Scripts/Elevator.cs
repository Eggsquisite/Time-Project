using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float waitTimer = 2f;
    [SerializeField] GameObject pathPrefab = null;
    [SerializeField] Collider2D sideCollider = null;

    private List<Transform> waypoints;
    private Collider2D passenger;
    private int wpIndex;
    private float waitModifier = 1f;
    private float baseWaitTime;
    private bool moving, waitStart, up;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GetWaypoints();
        baseWaitTime = waitTimer;
        waitStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            MoveToNextWaypoint();

        if (waitStart)
        {
            if (waitTimer > 0)
                waitTimer -= Time.deltaTime * waitModifier;
            else if (waitTimer <= 0)
                UnpauseMovement();
        }
    }

    private void MoveToNextWaypoint()
    {
        // Get value of current position
        var targetPosition = waypoints[wpIndex].transform.position;

        // Speed move speed independent of frame
        var movementThisFrame = moveSpeed * Time.deltaTime * waitModifier;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        if (transform.position == targetPosition)
            PauseMovement();
    }

    private void PauseMovement()
    {
        waitStart = true;
        sideCollider.enabled = true;

        // Stop movement
        if (moving)
        {
            waitStart = true;
            moving = false;
        }
    }

    private void UnpauseMovement()
    {
        waitStart = false;
        waitTimer = baseWaitTime;
        moving = true;
        sideCollider.enabled = false;

        if (wpIndex == waypoints.Count - 1)
            wpIndex--;
        else if (wpIndex < waypoints.Count - 1)
            wpIndex++;
    }

    public void SetWaitMod(float waitMod)
    {
        waitModifier = waitMod;
    }

    public void ResetWaitMod()
    {
        waitModifier = baseWaitTime;
    }

    private List<Transform> GetWaypoints()
    {
        var charWaypoints = new List<Transform>();

        // foreach (TYPE varname in VARIABLE)
        foreach (Transform child in pathPrefab.transform)
        {
            charWaypoints.Add(child);
        }

        return charWaypoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Civilian")
        {
            var charMovement = collision.gameObject.GetComponent<CivMovement>();
            Debug.Log("touching");
            //charMovement.OnElevator();
        }
        else if (collision.tag == "Enemy")
        {
            var enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();

            //enemyMovement.OnElevator();
        }
    }
}
