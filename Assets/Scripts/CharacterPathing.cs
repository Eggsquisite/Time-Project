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
    int waypointIndex = 0;
    int waitTimeIndex = 0;
    bool readyToMove = true;
    bool speedChange = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set position of character to start of path
        transform.position = waypoints[waypointIndex].transform.position;
        anim = GetComponent<Animator>();
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
        readyToMove = false;
        anim.SetBool("Moving", false);

        yield return new WaitForSeconds(waitTimes[waitTimeIndex]);
        if (waitTimeIndex < waitTimes.Count - 1)
            waitTimeIndex++;

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
    }

    public void OldMoveSpeed(float speedMultiplier)
    {
        if (speedChange == true)
        {
            moveSpeed /= speedMultiplier;
            speedChange = false;
        }
        else
            return;
    }
}
