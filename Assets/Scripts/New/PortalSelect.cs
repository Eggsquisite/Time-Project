using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalSelect : MonoBehaviour
{
    [SerializeField] GameObject portalObj;
    [SerializeField] Button b;

    private bool selected;
    private ObjectPlacement placer;
    Color tmp;

    private void Start()
    {
        placer = Camera.main.GetComponent<ObjectPlacement>();
        tmp = GetComponent<Image>().color;    
    }

    private void Update()
    {
        
    }

    public void SetSelected()
    {
        placer.SetSelected(portalObj);
        b.interactable = false;
    }
}
