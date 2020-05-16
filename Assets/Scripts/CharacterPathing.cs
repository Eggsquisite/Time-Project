using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathing : MonoBehaviour
{
    // Keep movespeed same so player can predict movement, only having to learn the pauses
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] List<float> waitTimes;

    Animator anim;
    SpriteRenderer sprite;
    private int waypointIndex = 0;
    private int waitTimeIndex = 0;
    private float normalSpeed = 0f;
    private bool readyToMove = true;
    private bool speedChange = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set position of character to start of path
        transform.position = waypoints[waypointIndex].transform.position;
        normalSpeed = moveSpeed;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
                StartCoroutine(WaitForNextMove());
                waypointIndex++;
            }
        }
    }

    IEnumerator WaitForNextMove()
    {
        // Stop movement and animation
        if (readyToMove)
        {
            readyToMove = false;
            anim.SetBool("Moving", false);
        }

        yield return new WaitForSeconds(waitTimes[waitTimeIndex]);

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

    public void NewMoveSpeed(float speedMultiplier)
    {
        moveSpeed *= speedMultiplier;
        speedChange = true;
        //sprite.sortingOrder = 10;
    }

    public void ResetMoveSpeed()
    {
        if (speedChange == true)
        {
            moveSpeed = normalSpeed;
            speedChange = false;
            //sprite.sortingOrder = 0;
        }
        else
            return;
    }

    public float GetWaitTime()
    {
        return waitTimes[waitTimeIndex];
    }

    public void SetWaitTime(float newWaitTime)
    {
        waitTimes[waitTimeIndex] = newWaitTime;

        // Restart coroutine with new wait time
        if (!readyToMove)
        {
            StopCoroutine(WaitForNextMove());
            StartCoroutine(WaitForNextMove());
        }
    }
}
