using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    AudioSource audioSource;

    private void Awake() {
        instance = this;
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    //이동 관련
    [Header("-이동-")]
    public float moveSpeed = 2.0f;
    public float turnSpeed = 4.0f;
    public float gravity = 9.8f;
    private float xRotate = 0.0f;
    public CharacterController controller;
    private Vector3 MoveDir;
    
    //줌 관련
    [Header("-줌-")]
    public float initialFOV;
    public float zoomFOV;
    public float zoomSwitchTime;
    public bool isZoom = false;

    //오브젝트 관련
    [Header("-오브젝트-")]
    public InvestigateController investigateController;
    public float objectDetectRange = 5f;

    //앉기 관련
    [Header("-앉기-")]
    private float initialHeight;
    public float crouchHeight = 1f;
    public float crouchSwitchTime = 0.5f;
    private bool isCrouch = false;
    
    [Header("-카메라-")]
    public GameObject playerCamera;
    public bool isCamReverse = false;

    //손전등 관련
    [Header("-손전등-")]
    public Light flashLight;
    
    //노트 관련
    [Header("-노트-")] public GameObject note;
    public bool isNote = false;
    public Texture2D menuCursorTexture;
    public GameObject fpsPanel;
    public GameObject investigatePanel;

    private void Start()
    {
        InterfaceManager.ShowFirstPersonUI();
        SetMouseCursor();
        investigateController = GetComponent<InvestigateController>();
        controller = GetComponent<CharacterController>();
        initialHeight = playerCamera.transform.position.y;
        Camera.main.fieldOfView = initialFOV;
    }

    private void Update()
    {
        ZoomInAndOut();

        if (!investigateController.enabled && !isNote)
        {
            Move();
            MouseRotation();
            Crouch();
            CheckObject();
            FlashLight();
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            OpenNote();
    }

    void SetMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ResetMouseCursor()
    {
        Cursor.visible = true;
        // Cursor.SetCursor(menuCursorTexture, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
    }

    void OpenNote()
    {
        this.audioSource.Play();

        // 노트가 활성화 되있지 않다면
        if (!isNote)
        {
            // 마우스 커서를 보이게 한다.
            ResetMouseCursor();

            // fpsPanel을 
            if(fpsPanel)
                fpsPanel.SetActive(false);
            if(investigatePanel)
                investigatePanel.SetActive(false);
            isNote = true;
            note.SetActive(true);
        }

        // 노트가 활성화 되있다면
        else
        {
            if(!investigateController.enabled)
            {
                fpsPanel.SetActive(true);
                SetMouseCursor();
            }
            else
                investigatePanel.SetActive(true);
            isNote = false;
            note.SetActive(false);
        }
    }

    void MouseRotation()
    {
        // 플레이어 회전 말고 카메라만 회전으로 변경
        float yRotate = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * turnSpeed;
        // float yRotate2 = flashLight.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * turnSpeed;

        if (!isCamReverse)
        {
            float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
            xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);
        }
        else
        {
            float xRotateSize = Input.GetAxis("Mouse Y") * turnSpeed;
            xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);
        }
        // transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        playerCamera.transform.localEulerAngles = new Vector3(xRotate, 0, 0);
        transform.localEulerAngles = new Vector3(0, yRotate, 0);
        
        // flashLight.transform.localEulerAngles = new Vector3(xRotate, yRotate2, 0);
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDir = transform.TransformDirection(MoveDir);

            // 위나 아래를 보고 이동시 점프되는것 방지
            MoveDir = new Vector3(MoveDir.x, 0, MoveDir.z);

            MoveDir *= moveSpeed;
        }

        MoveDir.y -= gravity * Time.deltaTime;
        controller.Move(MoveDir * Time.deltaTime);

        //flashLight.transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
    }

    void ZoomInAndOut()
    {
        // 토글 방식에서 누를때만 적용으로 변경
        if (Input.GetMouseButtonDown(1))
        {
            isZoom = true;
            Camera.main.DOFieldOfView(zoomFOV, zoomSwitchTime);
        }
        if (Input.GetMouseButtonUp(1))
        {
            isZoom = false;
            Camera.main.DOFieldOfView(initialFOV, zoomSwitchTime);        
        }

        // if (Input.GetMouseButtonDown(1) && Camera.main.fieldOfView == initialFOV)
        // {
        //     isZoom = true;
        //     Camera.main.DOFieldOfView(zoomFOV, switchTime);
        // }
        // else if (Input.GetMouseButtonDown(1) && Camera.main.fieldOfView == zoomFOV)
        // {
        //     isZoom = false;
        //     Camera.main.DOFieldOfView(initialFOV, switchTime);
        // }
    }

    void CheckObject()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * objectDetectRange);

        RaycastHit hit;

        int layerMask = 1 << LayerMask.NameToLayer("InteractObject");

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, objectDetectRange, layerMask))
        {
            GameObject gameObject = hit.collider.gameObject;
            InteractObject objectInteraction = gameObject.GetComponent<InteractObject>();

            if (objectInteraction != null)
            {
                // 모양이 바뀐다.
                CheckPointAnimation.instance.isActive = true;

                // 클릭하면
                if (Input.GetMouseButtonDown(0))
                {
                    this.audioSource.Play();
                    objectInteraction.Interact();
                }
            }
            else
                CheckPointAnimation.instance.isActive = false;
        }
        else
            CheckPointAnimation.instance.isActive = false;

    }

    // 앉기 동작
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
        isCrouch = !isCrouch;
        if (isCrouch)
        {
            //applySpeed = crouchSpeed;
            Camera.main.transform.DOMoveY(crouchHeight, crouchSwitchTime);
        }
        else
        {
            //applySpeed = walkSpeed;
            Camera.main.transform.DOMoveY(initialHeight, crouchSwitchTime);
        }
        }
    }

    private void FlashLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
            flashLight.enabled = !flashLight.enabled;            
    }

}
