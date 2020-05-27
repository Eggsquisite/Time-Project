﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float timeMultiplier = 2f;
    [SerializeField] bool constantMovement;

    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Time.timeScale = timeMultiplier;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            Time.timeScale = 1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (constantMovement)
            t.position = new Vector3(t.position.x + moveSpeed * Time.deltaTime, t.position.y, t.position.z);
        else
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
}
