using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    //ScreenResolution
    public enum ScreenResolution
    {
        res1, res2, res3
    }
    [SerializeField] ScreenResolution scrRes;
    public Text resText;

    //FullScreen
    public bool isFull = true;
    public Text fullText;

    //Cam
    PlayerController playerController;
    public Slider mouseSensitivitySlider;
    public Text sensitivityText;
    public Toggle mouseReverseToggle;

    //Audio
    public Slider volumeSlider;
    public Text volumeText;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        scrRes = ScreenResolution.res3;

        if (playerController != null)
            mouseSensitivitySlider.value = playerController.turnSpeed;
        volumeSlider.value = AudioListener.volume;
        ChangeResolution();
    }

    //�ػ� ����
    public void ChangeResolutionUI(bool right)
    {
        if (scrRes == ScreenResolution.res1)
        {
            if (right)
            {
                scrRes = ScreenResolution.res2;
                resText.text = "1024 x 768";
            }
            else
            {
                scrRes = ScreenResolution.res3;
                resText.text = "1920 x 1080";
            }
        }
        else if (scrRes == ScreenResolution.res2)
        {
            if (right)
            {
                scrRes = ScreenResolution.res3;
                resText.text = "1920 x 1080";
            }
            else
            {
                scrRes = ScreenResolution.res1;
                resText.text = "800 x 600";
            }
        }
        else if (scrRes == ScreenResolution.res3)
        {
            if (right)
            {
                scrRes = ScreenResolution.res1;
                resText.text = "800 x 600";
            }
            else
            {
                scrRes = ScreenResolution.res2;
                resText.text = "1024 x 768";
            }
        }
    }

    private void ChangeResolution()
    {
        switch (scrRes)
        {
            case ScreenResolution.res1:
                
                Screen.SetResolution(800, 600, true);
                break;
            case ScreenResolution.res2:
                Screen.SetResolution(1024, 768, true);
                break;
            case ScreenResolution.res3:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    //��üȭ�� ����
    public void ChangeFullScreenUI()
    {
        isFull = !isFull;
        fullText.text = isFull ? "��ü ȭ��" : "â���";
    }

    private void ChangeFullScreen()
    {
        switch (scrRes)
        {
            case ScreenResolution.res1:
                Screen.SetResolution(800, 600, isFull);
                break;
            case ScreenResolution.res2:
                Screen.SetResolution(1024, 768, isFull);
                break;
            case ScreenResolution.res3:
                Screen.SetResolution(1920, 1080, isFull);
                break;
        }
    }

    //�׸��� ����Ƽ ����

    //�ؽ��� �ػ� ����

    //ī�޶� �ΰ��� ����
    private void ChangeCamSensitivity()
    {
        playerController.turnSpeed = mouseSensitivitySlider.value;
    }

    //ī�޶� Y�� ���� ����
    private void ChangeCamReverse()
    {
        playerController.isCamReverse = mouseReverseToggle.isOn;
    }

    //�Ҹ� ũ�� ����
    private void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    //���� Ȯ��
    public void ConfirmSettings()
    {
        ChangeResolution();
        ChangeFullScreen();
        ChangeCamSensitivity();
        ChangeCamReverse();
    }

    private void Update()
    {
        //�ǽð� UI
        sensitivityText.text = mouseSensitivitySlider.value.ToString("F1");
        volumeText.text = volumeSlider.value.ToString("F1");
        //�ǽð� ����
        ChangeVolume();
    }
    
}
