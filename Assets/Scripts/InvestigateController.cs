using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InvestigateController : MonoBehaviour
{
    public static InvestigateController instance;

    public List<GameObject> InvestigatedObjects;

    public GameObject InvestigatingObject;

    public float InvestigateSpeed = 0.3f;

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        enabled = false;
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isNote)
            return;
        
        RotateObject();
        ShowText();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndInvestigate();

            if (InvestigatedObjects.Count != 0)
            {
                GameObject gameObject = InvestigatedObjects[InvestigatedObjects.Count - 1];
                StartInvestigate(gameObject);
                InvestigatedObjects.Remove(gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EndInvestigate();
            InvestigatedObjects.Clear();
        }
    }
    public bool isDraging;

    public float dragSpeed = 10f;

    public Vector3 oldDragPosition;

    public void RotateObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            int layerMask = 1 << LayerMask.NameToLayer("ChildInteractObject") | 1<< LayerMask.NameToLayer("InteractObject");

            if (Physics.Raycast(cast, out hit, 1, layerMask))
            {
                InvestigateObject childInteraction = hit.collider.gameObject.GetComponent<InvestigateObject>();

                List<Transform> child = new List<Transform>();
                for (int i = 0; i < InvestigatingObject.transform.childCount; i++)
                    child.Add(InvestigatingObject.transform.GetChild(i));

                // childInteraction을 갖고 있다면?
                if (childInteraction != null && child.Contains(childInteraction.transform))
                {
                    InvestigatedObjects.Add(InvestigatingObject);
                    EndInvestigate();
                    StartInvestigate(childInteraction.gameObject);
                    this.audioSource.Play();
                }
            }
            oldDragPosition = Input.mousePosition;
            isDraging = true;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 minus = (Input.mousePosition - oldDragPosition);
            Vector3 rotate = new Vector3(minus.y, -minus.x, 0);

            InvestigatingObject.transform.Rotate(rotate / Screen.dpi * dragSpeed, Space.Self);
            oldDragPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            oldDragPosition = Vector3.zero;
            isDraging = false;
        }

        // 드래그 시작
        // if (Input.GetMouseButtonDown(0) && isDraging == false)
        // {
        //     Debug.Log("드래그 시작");
        //     initDragPosition = Input.mousePosition;
        //     isDraging = true;
        // }
        // // 드래그 중
        // else if (Input.GetMouseButton(0) && isDraging == true)
        // {
        //     Debug.Log("드래그 중");
        //     Debug.Log((initDragPosition - Input.mousePosition) * dragSpeed);
        //     InvestigatingObject.transform.Rotate((initDragPosition - Input.mousePosition) * dragSpeed);
        // }
        // // 드래그 종료
        // else if (Input.GetMouseButtonUp(0) && isDraging == true)
        // {
        //     Debug.Log("드래그 종료");
        //     initDragPosition = Vector3.zero;
        //     isDraging = false;
        // }
    }

    public void ShowText()
    {
        if (Input.GetKeyDown(KeyCode.R))
            if (InterfaceManager.instance.TextPanel.activeSelf)
                InterfaceManager.HideTextUI();
            else
                InterfaceManager.ShowTextUI();
    }

    public void StartInvestigate(GameObject _gameObject)
    {
        InterfaceManager.ShowInvestigateUI(_gameObject.GetComponent<InvestigateObject>());

        if (PlayerController.instance.flashLight.enabled)
            PlayerController.instance.flashLight.enabled = false;

        enabled = true;

        InvestigatingObject = _gameObject;

        _gameObject.transform.DOMove(Camera.main.transform.position + Camera.main.transform.forward * 0.6f, InvestigateSpeed);
        _gameObject.transform.DOLookAt(Camera.main.transform.position, InvestigateSpeed);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        CheckPointAnimation.instance.gameObject.SetActive(false);
    }

    public void EndInvestigate()
    {
        InvestigateObject objectInteraction = InvestigatingObject.GetComponent<InvestigateObject>();
        InvestigatingObject.transform.DOLocalMove(objectInteraction.originPosition, InvestigateSpeed);
        InvestigatingObject.transform.DOLocalRotate(objectInteraction.originRotation, InvestigateSpeed);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        CheckPointAnimation.instance.gameObject.SetActive(true);

        enabled = false;
        InterfaceManager.ShowFirstPersonUI();
    }
}
