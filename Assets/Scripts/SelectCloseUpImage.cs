using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCloseUpImage : MonoBehaviour
{
    Image myImage;
    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (myImage.color == new Color(255, 255, 255, 255) && Input.GetMouseButtonDown(0))
        {
            myImage.color = new Color(255, 255, 255, 0);
            myImage.raycastTarget = false;
        }
    }
}
