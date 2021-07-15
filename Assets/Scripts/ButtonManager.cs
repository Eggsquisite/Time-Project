using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    [SerializeField] AudioClip scrollSound = null;
    [SerializeField] Text helpText = null;
    [SerializeField] Text portalCountTxt = null;
    [SerializeField] List<Button> portals = null;

    private int portalCount;
    //private Button b;
    private ObjectPlacement placer;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        placer = Camera.main.GetComponent<ObjectPlacement>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void SetSelected(GameObject portal, Button button)
    {
        //b = button;
        placer.SetSelected(portal);
        if (helpText != null)
            helpText.text = "Right Click-> Unselect";

        foreach(Button portalButton in portals)
        {
            portalButton.interactable = false;
        }
        audioSource.PlayOneShot(scrollSound);
    }

    public void Reset()
    {
        //b.interactable = true;
        if (portalCount > 0) { 
            foreach (Button portalButton in portals)
            {
                portalButton.interactable = true;
            }
            if (helpText != null)
                helpText.text = "Left Click-> Select";
        }
    }

    public void PortalsUsedUp() { 
        foreach(Button button in portals)
        {
            button.interactable = false;
        }
    }

    public void UpdatePortalUses(int newValue) {
        portalCount = newValue;
        portalCountTxt.text = portalCount.ToString();
    }
}
