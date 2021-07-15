using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveMode : MonoBehaviour
{
    [SerializeField] GameObject portalButtons;
    [SerializeField] GameObject observeUI;

    // Start is called before the first frame update
    void Start()
    {
        if (portalButtons != null) portalButtons.SetActive(false);
    }

    public void FinishObserveMode() {
        // called when player finishes observe mode
        GameManager.instance.SetObserveMode(false);
        if (portalButtons != null) portalButtons.SetActive(true);
        if (observeUI != null) observeUI.SetActive(false);
    }


}
