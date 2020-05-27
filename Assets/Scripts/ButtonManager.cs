using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button b;
    private ObjectPlacement placer;

    // Start is called before the first frame update
    void Start()
    {
        placer = Camera.main.GetComponent<ObjectPlacement>();
    }

    public void SetSelected(GameObject portal)
    {
        placer.SetSelected(portal);
    }

    public void Reset()
    {
        b.interactable = true;
    }
}
