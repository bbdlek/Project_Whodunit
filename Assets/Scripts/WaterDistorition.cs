using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDistorition : MonoBehaviour
{
    /// <summary>
    /// Distorition Material을 넣어줘야 작동함.
    /// </summary>
    [Header("Distorition Material")]
    public List<Material> materials = new List<Material>();

    
    /// <summary>
    /// 변경에 걸리는 시간
    /// </summary>
    public float strongTime = 2f;
    public float weakTime = 1f;

    public float changeTime = 2f;
    public float percentage = 0.4f;

    private void OnValidate() 
    {
        materials.Clear();
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            foreach (Material material in renderer.sharedMaterials)
            {
                if(material.shader.name.Equals("Shader Graphs/Distorition_shader"))
                    materials.Add(material);
            }
        }
    }

    private void Start() {
        foreach (var material in materials)
            material.SetFloat("_Amount", 0.4f);

    }

    private void Update() {

    }

    /// <summary>
    /// 회색 물체를 색깔 물체로 변경해주는 함수
    /// </summary>

    public void PourWater()
    {
        StartCoroutine(PourWaterCoroutine());
    }

    private IEnumerator PourWaterCoroutine()
    {
        while (percentage < 10f)
        {
            foreach (var material in materials)
                material.SetFloat("_Amount", percentage);

            percentage += (Time.deltaTime * 9.6f) / changeTime;

            yield return null;

        }

        while (percentage > 0.4f)
        {
            foreach (var material in materials)
                material.SetFloat("_Amount", percentage);

            percentage -= (Time.deltaTime *9.6f) / changeTime;

            yield return null;
        }

        yield return null;
    }
}
