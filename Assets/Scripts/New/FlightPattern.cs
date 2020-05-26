using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPattern : MonoBehaviour
{
    [SerializeField] GameObject pathPrefab;

    [Header ("Skree stats")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] List<float> waitTimes;

    private List<Transform> waypoints;
    private float waitModifier = 1f;
    private float waitTimer;
    private int wpIndex = 0;
    private int wtIndex = 0;
    private bool moveReady, waitStart, fly, retrace;

    // Start is called before the first frame update
    void Start()
    {
        // Set position of character to start of path
        waypoints = GetWaypoints();
        transform.position = waypoints[wpIndex].transform.position;
    }

    public void Fly()
    {
        if (!fly)
        {
            fly = true;
            moveReady = true;
        }
    }

    public void WaitMod(float waitMod)
    {
        waitModifier = waitMod;
    }

    void Update()
    {
        if (fly)
        {
            if (moveReady)
                MoveToNextWaypoint();

            if (waitStart)
            {
                if (waitTimer > 0)
                    waitTimer -= Time.deltaTime * waitModifier;
                else if (waitTimer <= 0)
                    UnpauseMovement();
            }
        }
    }

    private void MoveToNextWaypoint()
    {
        // Get value of current position
        var targetPosition = waypoints[wpIndex].transform.position;

        // Speed move speed independent of frame
        var movementThisFrame = moveSpeed * Time.deltaTime * waitModifier;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        // If retracing, face left, else face right
        if (!retrace)
            transform.rotation = new Quaternion(0, 0, 0, 0);
        else
            transform.rotation = new Quaternion(0, 180, 0, 0);

        if (transform.position == targetPosition)
            PauseMovement();
    }

    private void PauseMovement()
    {
        //waitTimer = 1;
        waitTimer = waitTimes[wtIndex];
        waitStart = true;

        // Stop movement
        if (moveReady)
        {
            waitStart = true;
            moveReady = false;
        }
    }

    private void UnpauseMovement()
    {
        waitStart = false;
        waitTimer = 0f;

        // Since waitTime.count is one less than the amount of waypoints.count
        if (wtIndex < waitTimes.Count - 1)
        { 
            //wtIndex++;
        }

        // Restart movement
        if (!retrace)
        {
            if (wpIndex < waypoints.Count - 1)
            {
                wpIndex++;
                wtIndex++;
                moveReady = true;
            }
            else if (wpIndex >= waypoints.Count - 1)
            {
                wpIndex--;
                wtIndex--;
                retrace = !retrace;
                moveReady = true;
            }
        }
        else
        {
            if (wpIndex == 0)
            {
                wpIndex++;
                wtIndex++;
                retrace = !retrace;
                moveReady = true;
            }
            else if (wpIndex > 0)
            {
                wpIndex--;
                wtIndex--;
                moveReady = true;
            }
        }
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
