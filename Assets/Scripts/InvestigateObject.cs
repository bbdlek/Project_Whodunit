using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateObject : InteractObject
{
    [TextArea(minLines:1, maxLines:10)]
    public string text = "";

    public Vector3 originPosition;
    public Vector3 originRotation;

    private void Awake()
    {
        originPosition = transform.localPosition;
        originRotation = transform.localRotation.eulerAngles;
    }

    public override void Interact()
    {
        ColorChange colorChange = GetComponent<ColorChange>();
        if (colorChange != null)
            colorChange.MonoToColor();

        InvestigateController.instance.StartInvestigate(gameObject);
    }
}
