using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveMode : MonoBehaviour
{
    [SerializeField] GameObject portals;

    // Start is called before the first frame update
    void Start()
    {
        if (portals != null) portals.SetActive(false);
    }

    public void FinishObserveMode() {
        // called when player finishes observe mode
        GameManager.instance.SetObserveMode(false);
        if (portals != null) portals.SetActive(true);
        gameObject.SetActive(false);
    }


}
