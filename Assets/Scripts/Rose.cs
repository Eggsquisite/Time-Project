using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : MonoBehaviour
{
    [SerializeField] CivMovement civ = null;
    [SerializeField] float hideTime = 2f;

    private Animator anim;
    private Hide feet;
    private bool hiding;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        feet = GetComponentInChildren<Hide>();
    }

    private void Update()
    {
        if (hiding)
        {
            if (hideTime > 0)
                hideTime -= Time.deltaTime * civ.GetWaitModifier();
            else if (hideTime <= 0)
            {
                hideTime = 0;
                hiding = false;
                civ.RoseReveal();
                feet.RoseReveal();
                anim.SetBool("moving", true);
            }
        }

    }

    private void OnMouseDown()
    {
        if (hideTime > 0)
        {
            hiding = true;
            civ.RoseHide();
            feet.RoseHide();
            anim.SetBool("moving", false);
        }
    }

}
