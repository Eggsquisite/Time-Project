using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathing : MonoBehaviour
{
    // Keep movespeed same so player can predict movement, only having to learn the pauses
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] List<float> waitTimes;

    Animator anim;
    Timer timer;
    private List<Transform> waypoints;
    private float normalSpeed = 0f;
    private float waitModifier = 1f;
    private float timerCount = 0f;
    private int waypointIndex = 0;
    private int waitTimeIndex = 0;
    private bool timerStart = false;
    private bool readyToMove = true;
    private bool timeAltered = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set position of character to start of path
        anim = GetComponent<Animator>();
        timer = GetComponentInChildren<Timer>();
        normalSpeed = moveSpeed;

        waypoints = GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToMove == true && waypointIndex < waypoints.Count)
        {
            // Get value of current position
            var targetPosition = waypoints[waypointIndex].transform.position;

            // Speed move speed independent of frame
            var movementThisFrame = moveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                StopMovement();
                //StartCoroutine("WaitForNextMove");
            }
        }

        if (timerCount > 0.0f && timerStart == true)
        {
            timerCount -= Time.deltaTime * waitModifier;
            timer.SetTime(timerCount / waitTimes[waitTimeIndex]);
        }
        else if (timerCount <= 0.0f && timerStart == true)
        {
            RestartMovement();
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

    private void StopMovement()
    {
        timer.gameObject.SetActive(true);
        timer.SetTimerStatus(true);
        timerCount = waitTimes[waitTimeIndex];
        waypointIndex++;

        // Stop movement and animation
        if (readyToMove)
        {
            timerStart = true;
            readyToMove = false;
            anim.SetBool("Moving", false);
        }
    }

    private void RestartMovement()
    {
        timerStart = false;
        timerCount = 0f;
        timer.gameObject.SetActive(false);
        timer.SetTimerStatus(false);

        // Since waitTime.count is one less than the amount of waypoints.count
        if (waitTimeIndex < waitTimes.Count - 1)
            waitTimeIndex++;

        // Restart movement/animation
        if (waypointIndex < waypoints.Count)
        {
            readyToMove = true;
            anim.SetBool("Moving", true);
        }
    }

        IEnumerator WaitForNextMove()
    {
        // Stop movement and animation
        if (readyToMove)
        {
            timerStart = true;
            readyToMove = false;
            anim.SetBool("Moving", false);
        }

        yield return new WaitForSeconds(timerCount);

        // Since waitTime.count is one less than the amount of waypoints.count
        if (waitTimeIndex < waitTimes.Count - 1)
        {
            waitTimeIndex++;
        }

        // Restart movement/animation
        if (waypointIndex < waypoints.Count)
        {
            readyToMove = true;
            anim.SetBool("Moving", true);
        }
    }

    public void AltTimeAffect(float speedMultiplier, Color newColor)
    {
        timeAltered = true;
        timer.SetColor(newColor);
        moveSpeed *= speedMultiplier;
    }

    public void ResetTime()
    {
        if (timeAltered == true)
        {
            timeAltered = false;
            timer.ResetColor();
            moveSpeed = normalSpeed;
        }
        else
            return;
    }

    public void SetWaitTime(float modifier)
    {
        waitModifier = modifier;
    }
}
