using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{
    [SerializeField] float snapXValue = 4f;
    [SerializeField] float snapYValue = 1f;
    
    private float yOffset = -1f;
    private float depth = 10;

    void Update()
    {
        float snapXInverse = 1 / snapXValue;
        float snapYInverse = 1 / snapYValue;

        float x, y, z;

        // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
        // so 1.45 to nearest .5 is 1.5
        x = Mathf.Round(transform.position.x * snapXInverse) / snapXInverse;
        y = Mathf.Round(transform.position.y * snapYInverse) / snapYInverse;
        z = depth;  // depth from camera

        transform.position = new Vector3(x, y + yOffset, z);
    }
}
