using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InvestigateLandline : InteractObject
{
    public GameObject chatTxtObj;
    private Text _chatTxt;
    public int curIndex = 0;
    public List<string> texts = new List<string>();

    public bool isChat = false;

    private void Start()
    {
        _chatTxt = chatTxtObj.GetComponentInChildren<Text>();
    }

    public override void Interact()
    {
        if (isChat)
            return;

        chatTxtObj.SetActive(true);        
        isChat = true;
        nextChat();
    }

    private void nextChat()
    {
        if (curIndex < texts.Count)
        {
            _chatTxt.text = "";
            _chatTxt.DOText(texts[curIndex], 2f);
            curIndex++;
        }
        else
            endChat();
    }

    private void endChat()
    {
        curIndex = 0;
        _chatTxt.text = "";
        chatTxtObj.SetActive(false);
        isChat = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if(isChat)
                nextChat();
        }

        if (isChat && Input.GetKeyDown(KeyCode.Q))
        {
            endChat();
        }
    }
}
