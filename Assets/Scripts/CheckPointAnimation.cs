using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAnimation : MonoBehaviour
{
    public static CheckPointAnimation instance;

    private void Awake()
    {
        instance = this;
    }

    public bool isActive = false;

    public float smallSize = 10;

    public float bigSize = 20;

    public float mySize = 10;

    public float animationTime = 0.2f;

    public RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isActive)
            mySize += Time.deltaTime * (bigSize - smallSize) / animationTime;
        else
            mySize -= Time.deltaTime * (bigSize - smallSize) / animationTime;

        mySize = Mathf.Clamp(mySize, smallSize, bigSize);

        rect.sizeDelta = new Vector2(mySize, mySize);
    }

    //public void ActiveAnimation()
    //{
    //    //Debug.Log("Ŀ����Ƽ��");

    //    // �۾����� �߿��� �۾����� �ڷ�ƾ ����
    //    if (inActiveCoroutine != null)
    //    {
    //        StopCoroutine(inActiveCoroutine);
    //    }
    //    // Ŀ���� ���� �ƴҶ� Ŀ���� �ڷ�ƾ ����
    //    if (activeCoroutine == null && mySize == smallSize)
    //    {
    //        activeCoroutine = ActiveCoroutine();
    //        StartCoroutine(activeCoroutine);
    //    }
    //}

    //public void InActiveAnimation()
    //{
    //    //Debug.Log("Ŀ�� �ξ�Ƽ��");

    //    // �۾����� �߿��� �۾����� �ڷ�ƾ ����
    //    if (activeCoroutine != null)
    //    {
    //        StopCoroutine(activeCoroutine);
    //    }
    //    // Ŀ���� ���� �ƴҶ� Ŀ���� �ڷ�ƾ ����
    //    if (inActiveCoroutine == null && mySize == bigSize)
    //    {
    //        inActiveCoroutine = InActiveCoroutine();
    //        StartCoroutine(inActiveCoroutine);
    //    }
    //}

    //public IEnumerator ActiveCoroutine()
    //{
    //    mySize = smallSize;

    //    RectTransform rect = GetComponent<RectTransform>();

    //    while (mySize < bigSize)
    //    {
    //        Debug.Log("Ŀ������");
    //        mySize += Time.deltaTime * (bigSize - smallSize) / animationTime;
    //        rect.sizeDelta = new Vector2(mySize, mySize);
    //        yield return null;
    //    }

    //    mySize = bigSize;
    //    rect.sizeDelta = new Vector2(mySize, mySize);
    //    activeCoroutine = null;
    //    yield return null;
    //}


    //public IEnumerator InActiveCoroutine()
    //{
    //    mySize = bigSize;

    //    RectTransform rect = GetComponent<RectTransform>();

    //    while (mySize > smallSize)
    //    {
    //        Debug.Log("�۾�������");
    //        mySize -= Time.deltaTime * (bigSize - smallSize) / animationTime;
    //        rect.sizeDelta = new Vector2(mySize, mySize);
    //        yield return null;
    //    }

    //    mySize = smallSize;
    //    rect.sizeDelta = new Vector2(mySize, mySize);
    //    inActiveCoroutine = null;
    //    yield return null;
    //}
}
