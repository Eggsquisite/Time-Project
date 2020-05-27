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

    private void Start()
    {
        b.interactable = on;
    }

    public void SetSelected()
    {
        bm.SetSelected(portalPrefab, b);
        b.interactable = !on;
    }
}
