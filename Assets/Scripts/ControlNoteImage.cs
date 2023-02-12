using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlNoteImage : MonoBehaviour, IPointerClickHandler
{
    PlayerController playerController;
    public GameObject closeUpImageObj;
    Image closeUpImage;
    public Sprite myImage;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        closeUpImageObj = GameObject.Find("ImageCloseUp");
        closeUpImage = closeUpImageObj.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        if (!playerController.isNote)
            return;

        myImage = GetComponent<Image>().sprite;
        if (myImage == null)
            return;

        //Debug.Log("Image Selected");
        closeUpImage.color = new Color(255, 255, 255, 255);
        closeUpImage.sprite = myImage;
        closeUpImage.raycastTarget = true;
    }

}
