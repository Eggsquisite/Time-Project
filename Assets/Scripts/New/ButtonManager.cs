using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] List<Button> buttons;

    private ObjectPlacement placer;

    // Start is called before the first frame update
    void Start()
    {
        placer = Camera.main.GetComponent<ObjectPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelected(GameObject portal)
    {
        placer.SetSelected(portal);
    }
}
