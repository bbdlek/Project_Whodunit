using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractDoor : InteractObject
{
    public bool isRotated;

    public Vector3 originRotation;

    public Vector3 changedRotation;
    
    private void Start() 
    {
        transform.DOLocalRotate(originRotation, 1f);
        isRotated = false;
    }

    public override void Interact()
    {
        if (!isRotated)
        {
            transform.DOLocalRotate(changedRotation, 1f);
            isRotated = true;
        }
        else
        {
            transform.DOLocalRotate(originRotation, 1f);
            isRotated = false;
        }
    }
}
