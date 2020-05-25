using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalSelect : MonoBehaviour
{
    [SerializeField] GameObject portalObj;
    [SerializeField] ButtonManager bm;

    public void SetSelected()
    {
        bm.SetSelected(portalObj);
    }
}
