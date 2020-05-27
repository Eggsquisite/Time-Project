using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float pauseMovement = 0.02f;
    [SerializeField] float timeMultiplier = 2f;
    [SerializeField] bool constantMovement;

    private Transform t;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !paused)
            Pausing();
        else if (Input.GetKeyDown(KeyCode.Space) && paused)
            Unpausing();
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
        else if (paused)
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
