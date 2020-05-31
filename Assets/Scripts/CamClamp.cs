using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamClamp : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float xMin = -25;
    [SerializeField] float xMax = 25;
    [SerializeField] float yVal = 3.5f;

    Transform t;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp(target.position.x, xMin, xMax);

        t.position = new Vector3(x, yVal, t.position.z);
    }

    public void SetMoving(bool status)
    {
        moving = status;
    }

    public bool GetMoving()
    {
        return moving;
    }
}
