using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPattern : MonoBehaviour
{
    [SerializeField] GameObject pathPrefab = null;
    [SerializeField] GameObject carry = null;
    [SerializeField] bool endGame;

    [Header ("Skree stats")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] List<float> waitTimes = null;

    private List<Transform> waypoints;
    private float waitModifier = 1f;
    private float waitTimer;
    private int wpIndex = 0;
    private int wtIndex = 0;
    private bool moveReady, waitStart, fly, retrace;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Set position of character to start of path
        waypoints = GetWaypoints();
        transform.position = waypoints[wpIndex].transform.position;
        lastPosition = transform.position;
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

    public void Attacking(bool attack, float attackTimer)
    {
        if (attack)
            PauseMovement(attackTimer);
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

        if (moveReady)
        {
            var direction = transform.position - lastPosition;
            var localDirection = transform.InverseTransformDirection(direction);
            lastPosition = transform.position;

            if (direction.x > 0)
                transform.rotation = new Quaternion(0, 0, 0, 0);
            else if (direction.x <= 0)
                transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    private void Drop()
    {
        if (carry != null && !endGame)
        {
            carry.GetComponent<Rigidbody2D>().gravityScale = 2f;
            carry.GetComponent<Collider2D>().enabled = true;
            carry.GetComponent<EnemyMovement>().enabled = true;
            carry.transform.parent = null;
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
        //if (!retrace)
            //transform.rotation = new Quaternion(0, 0, 0, 0);
        //else
            //transform.rotation = new Quaternion(0, 180, 0, 0);

        if (transform.position == targetPosition)
            PauseMovement();
    }

    private void PauseMovement(float attackTimer)
    {
        waitTimer = attackTimer;
        waitStart = true;

        // Stop movement
        if (moveReady)
        {
            waitStart = true;
            moveReady = false;
        }
    }

    private void PauseMovement()
    {
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
                Drop();
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
            if (!child.name.Contains("Skree"))
                charWaypoints.Add(child);
        }

        return charWaypoints;
    }
}
