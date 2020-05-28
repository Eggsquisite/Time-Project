using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float pauseMoveSpeed = 0.02f;
    
    private Transform t = null;
    private float baseMoveSpeed;


    private void Start()
    {
        t = Camera.main.transform;
        baseMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            moveSpeed = pauseMoveSpeed;
        else if (Time.timeScale > 0)
            moveSpeed = baseMoveSpeed;
    }

    private void OnMouseOver()
    {
        Debug.Log("Moving left");
        t.position = new Vector3(t.position.x - moveSpeed * Time.deltaTime, t.position.y, t.position.z);
    }
}
