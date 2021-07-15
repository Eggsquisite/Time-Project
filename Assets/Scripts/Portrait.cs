using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    [SerializeField] Transform civ = null;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Vector3 offset;

    Transform t;
    private CamClamp cam;
    private bool followTarget;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
        cam = Camera.main.GetComponent<CamClamp>();
        
        //if (civ != null && civ.name.Contains("Jack"))
            //FollowTarget();
    }

    // Update is called once per frame
    void LateUpdate()
    {
/*        if (Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            followTarget = false;
        }
        else if (cam.GetMoving())
            followTarget = false;
        else if (followTarget && !cam.GetMoving())
        {
            if (civ != null)
            {
                Vector3 desiredPosition = civ.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(t.position, desiredPosition, moveSpeed * Time.deltaTime);
                t.position = smoothedPosition;
            }
        }*/
    }

    public void FollowTarget()
    {
        //if (civ != null)
            //followTarget = true;
    }
}
