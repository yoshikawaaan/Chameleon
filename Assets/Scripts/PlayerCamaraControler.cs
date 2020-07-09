using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamaraControler : MonoBehaviour
{
    //attach to player
    
    
    [SerializeField]private Transform playerTransform;
    [SerializeField]private Transform pivotTransform;
    [SerializeField]private Camera playerCamera;
    
    [SerializeField] private float cameraYaxisSpeed = 100.0f;
    [SerializeField] private float cameraXaxisSpeed = 100.0f;
    [SerializeField] private float cameraMoveSpeed = 1.0f;
    [SerializeField] private float marginRange = 0.0f;

    private float mouseX, mouseY;
    
    
    public Camera PlayerCamera
    {
        get => playerCamera;
        private set => playerCamera = value;
    }

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.Log("Please set playerCamera!!");
            Debug.Log("playerCamera setted Camera.main, Temporary.");
            playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera == null)
                playerCamera = Camera.main;
        }

    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
    
    void FixedUpdate()
    {
        Vector3 tmp = Vector3.Scale(playerCamera.transform.forward , new Vector3(1, 0, 1)).normalized;
        Debug.Log(tmp);
        //プレイヤーとの距離を一定に保つ
        //MoveCamera();
        RotateCamera();
        UpdateCamera();
    }

    void RotateCamera()
    {
        transform.Rotate(0, mouseX * cameraXaxisSpeed, 0);
        pivotTransform.Rotate(mouseY * -cameraYaxisSpeed, 0, 0);

    }
    void UpdateCamera()
    {
        Vector3 toPlayerVec = playerTransform.position - transform.position;
        float sqrLength = toPlayerVec.sqrMagnitude;
        if (sqrLength <= marginRange * marginRange)
            return;

        //Vector3 playerPos = playerTransform.position;
        Vector3 playerPos = playerTransform.position - toPlayerVec.normalized * marginRange;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, cameraMoveSpeed * Time.deltaTime);
    }

}