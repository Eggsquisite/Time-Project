using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamMovement : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float pauseMovement = 0.02f;
    [SerializeField] float moveMultiplier = 2f;
    [SerializeField] bool constantMovement = false;

    [Header("UI Buttons")]
    [SerializeField] float timeMultiplier;
    [SerializeField] Button speedUpButton;
    [SerializeField] Text speedChevrons;
    [SerializeField] Button slowDownButton;
    [SerializeField] Text slowChevrons;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject unpauseButton;

    private Transform t;
    private int speedIndex;
    private bool paused, speeding;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Confined;
        moveMultiplier = 3f;
        UpdateChevrons();
    }

    private void Update()
    {
        if (Time.timeScale > 0)
            paused = false;

/*        if (Input.GetKeyDown(KeyCode.Space) && !pauseMenu.activeSelf && !paused)
            Pausing();
        else if (Input.GetKeyDown(KeyCode.Space) && !pauseMenu.activeSelf && paused)
            Unpausing();

        if (Input.GetKey(KeyCode.LeftShift) && !speeding)
            SpeedUp();
        else if (Input.GetKeyUp(KeyCode.LeftShift) && speeding)
            NormalSpeed();*/
    }

    public void SpeedUp()
    {
        // called during speedUp button event
        //speeding = true;
        if (speedIndex < 3)
        {
            speedIndex++;
            slowDownButton.interactable = true;

            UpdateChevrons();
            if (speedIndex >= 3)
                speedUpButton.interactable = false;
        }

        //moveSpeed *= moveMultiplier;
        moveSpeed = 1 + (speedIndex * timeMultiplier);
        if (!paused) 
            Time.timeScale = 1 + (speedIndex * timeMultiplier);
    }

    public void SlowSpeed()
    {
        //speeding = false;
        if (speedIndex > -3) {
            speedIndex--;
            speedUpButton.interactable = true;

            UpdateChevrons();
            if (speedIndex <= -3)
                slowDownButton.interactable = false;
        }

        moveSpeed = 1 + (speedIndex * timeMultiplier);
        if (!paused) 
            Time.timeScale = 1 + (speedIndex * timeMultiplier);
    }

    public void ResetTime() {
        speedIndex = 0;
        UpdateChevrons();
        Time.timeScale = 1f;

        speedUpButton.interactable = true;
        slowDownButton.interactable = true;
        if (!paused) {
            pauseButton.SetActive(true);
            unpauseButton.SetActive(false);
        }
    }

    public void Pausing()
    {
        paused = true;
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        unpauseButton.SetActive(true);
    }

    public void Unpausing()
    {
        paused = false;
        Time.timeScale = 1 + (speedIndex * timeMultiplier);
        pauseButton.SetActive(true);
        unpauseButton.SetActive(false);
    }

    private void UpdateChevrons() {
        if (speedIndex == -3)
        {
            speedChevrons.text = "";
            slowChevrons.text = "<<<";
        }
        else if (speedIndex == -2)
        {
            speedChevrons.text = "";
            slowChevrons.text = "<<";
        }
        else if (speedIndex == -1)
        {
            speedChevrons.text = "";
            slowChevrons.text = "<";
        }
        else if (speedIndex == 0) {
            speedChevrons.text = "";
            slowChevrons.text = "";
        }
        else if (speedIndex == 1)
        {
            speedChevrons.text = ">";
            slowChevrons.text = "";
        }
        else if (speedIndex == 2)
        {
            speedChevrons.text = ">>";
            slowChevrons.text = "";
        }
        else if (speedIndex == 3)
        {
            speedChevrons.text = ">>>";
            slowChevrons.text = "";
        }
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
