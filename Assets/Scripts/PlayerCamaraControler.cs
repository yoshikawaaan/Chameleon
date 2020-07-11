using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerCamaraControler : MonoBehaviour
{
    //attach to player


    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Transform pivotTransform = null;
    [SerializeField] private Camera playerCamera = null;
    
    [SerializeField] private float cameraYaxisSpeed = 5.0f;
    [SerializeField] private float cameraXaxisSpeed = 10.0f;
    [SerializeField] private float cameraMoveSpeed = 5.0f;
    [SerializeField] private float marginRange = 0.5f;
    [SerializeField] private float angleRange = 60.0f;

    private float mouseX, mouseY;
    
    public Camera PlayerCamera
    {
        get => playerCamera;
        private set => playerCamera = value;
    }

    void Start()
    {
        SetUpCamera();
    }

    private void SetUpCamera()
    {
        //Inspector上で設定しなかった場合のnullチェックと設定
        if (playerCamera == null)
        {
            Debug.Log("Warning: Please set playerCamera.");
            Debug.Log("playerCamera setted Camera.main, Temporary.");
            playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera == null)
                playerCamera = Camera.main;
            if (playerCamera == null)
            {
                Debug.Log("Error: PlayerCamera Setting is Failed.");
                return;
            }
        }
        
        playerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
    
    void FixedUpdate()
    {
        RotateCamera(angleRange);
        MoveCamera();
    }

    void RotateCamera(float angleLimit)
    {
        //横回転
        transform.Rotate(0.00f, mouseX * cameraXaxisSpeed, 0.00f);
        
        //縦回転
        pivotTransform.Rotate(mouseY * -cameraYaxisSpeed, 0.00f, 0.00f);
        
        //縦回転制限範囲をオーバーしたら修正する。
        float upAngleLimit = angleLimit;//上方向制限角度
        float downAngleLimit = 360.0f - angleLimit;//下方向制限角度
        float pivotAngleX = pivotTransform.localEulerAngles.x;
        
        if (pivotAngleX > upAngleLimit && pivotAngleX < 90)
            pivotAngleX = upAngleLimit;
        else if (pivotAngleX < downAngleLimit && pivotAngleX > 270)
            pivotAngleX = downAngleLimit;
        
        /*
        EulerAnglesでは丸め込み誤差が出る(?)
        xだけ上記if文で変更してもy,zで0.05前後の誤差が発生した
        旧処理：pivotTransform.localEulerAngles = pivotEulerAngles;
        以下のようにVector3.zeroで誤差がないようにする
        */
        
        Vector3 pivotAngles = Vector3.zero;
        pivotAngles.x = pivotAngleX;
        pivotTransform.localEulerAngles = pivotAngles;
    }
    void MoveCamera()
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