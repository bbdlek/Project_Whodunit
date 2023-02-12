using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateToilet : InteractObject
{
    private int ClickCount = 0;
    public GameObject HiddenObject;

    private GameObject water;

    AudioSource audioSource;

    private void Awake()
    {
        HiddenObject.SetActive(false);
        water = transform.Find("water").gameObject;
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        //rend = water.GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Distorition_mt");
        //float amount = Mathf.PingPong(Time.time, 10.0f);
        //rend.material.SetFloat("_Amount", amount);

        //water.GetComponent<Renderer>().material.SetFloat("_Amount", 10f);

        //<summary>
        //물 내리는 소리 들리게 함
        //</summary>
        this.audioSource.Play();

        //<summary>
        //물 쉐이더 보이게 함
        //</summary>
        WaterDistorition waterDistorition = GetComponent<WaterDistorition>();
        if (waterDistorition != null)
        {
            //waterDistorition.StrongWater();
            //waterDistorition.WeakWater();
            waterDistorition.PourWater();
        }

        //<summary>
        //Click을 5번 했을 때 유언장을 보이게 함
        //</summary>
        ClickCount++;
        Debug.Log(ClickCount);
        if (ClickCount == 5)
        {
            HiddenObject.SetActive(true);
        }
    }
}
