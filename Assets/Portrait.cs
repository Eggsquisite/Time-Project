using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    [SerializeField] Transform civ;
    [SerializeField] float moveSpeed = 10f;

    Transform t;
    private bool findTarget, followTarget;
    private float lerp = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)         || 
            Input.GetKeyDown(KeyCode.LeftArrow) || 
            Input.GetKeyDown(KeyCode.D)         || 
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Stop following");
            findTarget = false;
            followTarget = false;
        }

        if (findTarget && civ.position.x < t.position.x + 0.5 && civ.position.x > t.position.x - 0.5)
        {
            Debug.Log("Start following " + civ.name);
            findTarget = false;
            followTarget = true;
        }
        else if (findTarget && civ.position.x >= t.position.x)
        {
            t.position = new Vector3(t.position.x + moveSpeed * Time.deltaTime, t.position.y, t.position.z);
        }
        else if (findTarget && civ.position.x < t.position.x)
        {
            t.position = new Vector3(t.position.x - moveSpeed * Time.deltaTime, t.position.y, t.position.z);
        }

        if (followTarget)
        {
            t.position = new Vector3(civ.position.x, t.position.y, t.position.z);
        }

        /*
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
            */
    }

    public void FindTarget()
    {
        findTarget = true;
    }
}
