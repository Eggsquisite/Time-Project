using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        t.position = new Vector2(t.position.x + moveSpeed * Time.deltaTime, t.position.y);
    }
}
