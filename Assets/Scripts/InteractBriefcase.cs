using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBriefcase : InvestigateObject
{
    Animator anim;

    public bool isOpened = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public override void Interact()
    {
        if (!isOpened)
        {
            anim.SetBool("isOpen", true);
            isOpened = true;
        }
        InvestigateController.instance.StartInvestigate(gameObject);
        /*else
        {
            isOpened = false;
            anim.SetBool("isOpen", false);
        }*/
    }
}
