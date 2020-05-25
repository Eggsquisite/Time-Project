using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    private Transform t;
    private bool doubleSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed = moveSpeed * 2;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = moveSpeed / 2;
    }

    // Update is called once per frame
    void LateUpdate()
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
}
