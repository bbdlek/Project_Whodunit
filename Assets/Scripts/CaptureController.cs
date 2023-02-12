using System;
using System.Collections;
using System.Collections.Generic;
using BookCurlPro;
using BookCurlPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class CaptureController : MonoBehaviour
{
    public Image ui_Image;
    public int page;
    public GameObject Book;

    //flash
    public float flashTimelength = .1f;
    public bool doCameraFlash = false;

    ///////////////////////////////////////////////////
    private Image flashImage;
    private float startTime;
    private bool flashing = false;

    void Start()
    {
        flashImage = GameObject.Find("flashImage").GetComponent<Image>();
        Color col = flashImage.color;
        col.a = 0.0f;
        flashImage.color = col;
    }

    IEnumerator charkack() {
        yield return new WaitForEndOfFrame();
        Texture2D img = ScreenCapture.CaptureScreenshotAsTexture();
        Rect rect = new Rect(0, 0, img.width, img.height);
        ui_Image = Book.transform.Find("Page" + page).Find("Image").GetComponent<Image>();
        ui_Image.sprite = Sprite.Create(img, rect, Vector2.one * .5f);
        ui_Image.color = new Color(255, 255, 255, 255);
        Book.transform.Find("Page" + page).Find("MaskingTape").transform.gameObject.SetActive(true);
        page++;
        if (page % 2 == 0 && page > 2)
        {
            Book.GetComponent<AddPagesDynamically>().AddPaper(Book.GetComponent<BookPro>(), page);
        }
    }

    public void Snapshot() {
        StartCoroutine(charkack());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !Book.activeInHierarchy)
        {
            Invoke("CameraFlash", 0.1f);
            //CameraFlash();
            Snapshot();
        }
    }

    public void CameraFlash()
    {
        // initial color
        Color col = flashImage.color;

        // start time to fade over time
        startTime = Time.time;

        // so we can flash again
        doCameraFlash = false;

        // start it as alpha = 1.0 (opaque)
        col.a = 1.0f;

        // flash image start color
        flashImage.color = col;

        // flag we are flashing so user can't do 2 of them
        flashing = true;

        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        bool done = false;

        while (!done)
        {
            float perc;
            Color col = flashImage.color;

            perc = Time.time - startTime;
            perc = perc / flashTimelength;

            if (perc > 1.0f)
            {
                perc = 1.0f;
                done = true;
            }

            col.a = Mathf.Lerp(1.0f, 0.0f, perc);
            flashImage.color = col;
            flashing = true;

            yield return null;
        }

        flashing = false;

        yield break;
    }


}
