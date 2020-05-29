using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float pauseMovement = 0.02f;
    [SerializeField] float moveMultiplier = 2f;
    [SerializeField] bool constantMovement = false;

    private Transform t;
    private bool paused, speeding;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
            paused = false;

        if (Input.GetKeyDown(KeyCode.Space) && !pauseMenu.activeSelf && !paused)
            Pausing();
        else if (Input.GetKeyDown(KeyCode.Space) && !pauseMenu.activeSelf && paused)
            Unpausing();

        if (Input.GetKey(KeyCode.LeftShift) && !speeding)
            SpeedUp();
        else if (Input.GetKeyUp(KeyCode.LeftShift) && speeding)
            NormalSpeed();
            
    }

    private void SpeedUp()
    {
        speeding = true;
        moveSpeed *= moveMultiplier;

        // For testing purposes
        Time.timeScale = moveMultiplier;
    }

    private void NormalSpeed()
    {
        speeding = false;
        moveSpeed /= moveMultiplier;

        Time.timeScale = 1f;
    }

    private void Pausing()
    {
        paused = true;
        Time.timeScale = 0;
    }

    private void Unpausing()
    {
        paused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (constantMovement)
            t.position = new Vector3(t.position.x + moveSpeed * Time.deltaTime, t.position.y, t.position.z);
        else if (!paused)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                t.position = new Vector3(t.position.x - moveSpeed * Time.deltaTime, t.position.y, t.position.z);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                t.position = new Vector3(t.position.x + moveSpeed * Time.deltaTime, t.position.y, t.position.z);
            }
        }
        else if (paused && !pauseMenu.activeSelf)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                t.position = new Vector3(t.position.x - moveSpeed * pauseMovement, t.position.y, t.position.z);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                t.position = new Vector3(t.position.x + moveSpeed * pauseMovement, t.position.y, t.position.z);
            }
        }
    }
}
