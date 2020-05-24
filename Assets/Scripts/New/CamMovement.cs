using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
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
