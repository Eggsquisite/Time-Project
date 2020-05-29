using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamClamp : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float xMin = -25;
    [SerializeField] float xMax = 25;

    Transform t;
    private float yVal;

    // Start is called before the first frame update
    void Awake()
    {
        t = transform;
        yVal = t.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp(target.position.x, xMin, xMax);
        float y = Mathf.Clamp(target.position.y, yVal, yVal);
        float z = Mathf.Clamp(target.position.z, -10, -10);

        t.position = new Vector3(x, y, z);
    }
}
