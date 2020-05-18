using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItem : MonoBehaviour
{
    GameObject childItem = null;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInChildren<GameObject>() != null)
            childItem = GetComponentInChildren<GameObject>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
