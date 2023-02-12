using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    public GameObject FirstPersonUI;
    public GameObject InvestigateUI;

    public GameObject TextPanel;
    public Text textUI;

    private void Awake()
    {
        instance = this;        
    }

    public static void ShowTextUI()
    {
        instance.TextPanel.SetActive(true);
    }

    public static void HideTextUI()
    {
        instance.TextPanel.SetActive(false);
    }

    public static void ShowFirstPersonUI()
    {
        instance.FirstPersonUI.SetActive(true);
        instance.InvestigateUI.SetActive(false);
    }

    public static void ShowInvestigateUI(InvestigateObject investigateObject)
    {
        instance.textUI.text = investigateObject.text;
        instance.FirstPersonUI.SetActive(false);
        instance.InvestigateUI.SetActive(true);
        HideTextUI();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Title Scene");
    }

}
