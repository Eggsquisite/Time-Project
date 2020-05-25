using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalSelect : MonoBehaviour
{
    [SerializeField] GameObject portalObj;
    [SerializeField] ButtonManager bm;
    [SerializeField] Button b;
    [SerializeField] bool on = true;

    private void Start()
    {
        b.interactable = on;
    }

    public void SetSelected()
    {
        bm.SetSelected(portalObj, b);
    }
}
