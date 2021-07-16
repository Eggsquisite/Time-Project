using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveMode : MonoBehaviour
{
    [SerializeField] GameObject portalButtons;
    [SerializeField] GameObject observeUI;
    [SerializeField] private CamMovement cam;

    // Start is called before the first frame update
    void Start()
    {
        if (portalButtons != null) portalButtons.SetActive(false);
    }

    public void FinishObserveMode() {
        // called when player finishes observe mode
        cam.ResetTime();
        GameManager.instance.SetObserveMode(false);
        if (portalButtons != null) portalButtons.SetActive(true);
        if (observeUI != null) observeUI.SetActive(false);
    }


}
