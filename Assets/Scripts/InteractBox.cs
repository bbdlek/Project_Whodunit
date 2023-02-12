using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractBox : InteractObject
{

    public bool isMoved = false;

    public Vector3 originPosition;

    public Vector3 moveVector;

    private void Start() 
    {
        transform.DOLocalMove(originPosition, 1f);
        isMoved = false;
    }

    public override void Interact()
    {
        if (!isMoved)
        {
            transform.DOLocalMove(moveVector, 1f);
            isMoved = true;
        }
        else
        {
            transform.DOLocalMove(originPosition, 1f);
            isMoved = false;
        }
    }

}
