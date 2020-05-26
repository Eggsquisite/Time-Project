using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPattern : MonoBehaviour
{
    [SerializeField] GameObject pathPrefab;
    [SerializeField] List<float> waitTimes;

    [Header ("Skree stats")]
    [SerializeField] float moveSpeed = 2f;

    private List<Transform> waypoints;
    private float waitModifier = 1f;
    private float baseSpeed, waitTimer;
    private int wpIndex, wtIndex;
    private bool moveReady, waitStart, fly;

    // Start is called before the first frame update
    void Start()
    {
        // Set position of character to start of path
        baseSpeed = moveSpeed;

        waypoints = GetWaypoints();
        transform.position = waypoints[wpIndex].transform.position;
    }

    public void Fly()
    {
        if (!fly)
            fly = true;
    }

    public void WaitMod(float waitMod)
    {
        waitModifier = waitMod;
    }

    void Update()
    {
        if (fly)
        {
            if (moveReady == true && wpIndex < waypoints.Count)
            {
                MoveToNextWaypoint();
            }


            if (waitTimer > 0.0f && waitStart)
            {
                Debug.Log("Fly skree");
                waitTimer -= Time.deltaTime * waitModifier;
            }
            else if (waitTimer > 0.0f && waitStart)
            {
                waitTimer += Time.deltaTime * waitModifier;
            }
            else if (waitTimer <= 0.0f && waitStart == true)
            {
                RestartMovement();
            }
        }
    }

    private void MoveToNextWaypoint()
    {
        // Get value of current position
        var targetPosition = waypoints[wpIndex].transform.position;

        // Speed move speed independent of frame
        var movementThisFrame = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        if (transform.position == targetPosition)
        {
            StopMovement();
            //StartCoroutine("WaitForNextMove");
        }
    }

    private void StopMovement()
    {
        waitTimer = waitTimes[wtIndex];
        wpIndex++;

        // Stop movement
        if (moveReady)
        {
            waitStart = true;
            moveReady = false;
        }
    }

    private void RestartMovement()
    {
        waitStart = false;
        waitTimer = 0f;

        // Since waitTime.count is one less than the amount of waypoints.count
        if (wtIndex < waitTimes.Count - 1)
            wtIndex++;

        // Restart movement
        if (wtIndex < waypoints.Count)
            moveReady = true;
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
}
