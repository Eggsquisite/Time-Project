using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTimer : MonoBehaviour
{
    [SerializeField] GameObject parentTimer = null;

    private void Delete()
    {
        Destroy(parentTimer);
    }
}
