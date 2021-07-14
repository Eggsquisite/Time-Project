using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalSelect : MonoBehaviour
{
    [SerializeField] GameObject portalPrefab = null;
    [SerializeField] ButtonManager bm = null;
    [SerializeField] Button b = null;
    [SerializeField] bool on = true;
    private ObjectPlacement op;


    private void Start()
    {
        if (op == null) op = ObjectPlacement.instance;
        b.interactable = on;
    }

    public void SetSelected()
    {
        // Called thru button event, sends data to ButtonManager
        bm.SetSelected(portalPrefab, b);
        //b.interactable = !on;
    }
}
