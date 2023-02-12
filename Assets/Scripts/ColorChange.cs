using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    /// <summary>
    /// MonoDissolve Material을 넣어줘야 작동함.
    /// </summary>
    [Header("MonoDissolve Material")]
    public List<Material> materials = new List<Material>();

    /// <summary>
    /// 이 물체는 색깔 물체인가
    /// </summary>
    public bool isColored = false;
    
    /// <summary>
    /// 변경에 걸리는 시간
    /// </summary>
    public float changeTime = 2f;


    private void OnValidate() 
    {
        materials.Clear();
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            foreach (Material material in renderer.sharedMaterials)
            {
                if(material.shader.name.Equals("Unagi11/MonoDissolve"))
                    materials.Add(material);
            }
        }
    }

    private void Start() {
        foreach (var material in materials)
        {
            material.SetFloat("_Amount", 0f);
            material.SetColor("_EdgeColor", new Color(0,0,0,0));
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F1))
            MonoToColor();
    }

    /// <summary>
    /// 회색 물체를 색깔 물체로 변경해주는 함수
    /// </summary>
    public void MonoToColor()
    {
        if (!isColored && materials[0].GetFloat("_Amount") < 0.5)
        {
            foreach (var material in materials)
            {
                material.SetColor("_EdgeColor", Color.red * Mathf.Pow(2, 20));
            }
            
            StartCoroutine(monoToColorCoroutine());
        }
    }

    private IEnumerator monoToColorCoroutine()
    {
        float percentage = 0f;

        while (percentage < 1f)
        {
            foreach (var material in materials)
                material.SetFloat("_Amount", percentage);

            percentage += Time.deltaTime / changeTime;

            yield return null;
        }

        isColored = true;

        yield return null;
    }
}
