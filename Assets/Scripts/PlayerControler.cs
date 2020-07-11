using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControler : MonoBehaviour
{
    private PlayerMover playerMover;
    private IPlayerInput playerInput;

    private Camera playerCamera;
    [SerializeField]private PlayerCamaraControler playerCamaraControler;

    [SerializeField] private float moveSpeed = 150f;
    [SerializeField] private float runSpeed = 500f;
    
    private Vector3 latestPos;
    
    void Start()
    {
        playerMover = new PlayerMover(GetComponent<Rigidbody>());
        playerInput = new PlayerInput();

        playerCamera = playerCamaraControler.PlayerCamera;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        playerInput.Inputting();
    }

    void FixedUpdate()
    {
        LookForward();
        MoveToCameraForward(playerInput.MoveDirection(), moveSpeed);
    }

    void MoveToCameraForward(Vector3 dir,float speed)
    {
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        dir = cameraForward * dir.z + playerCamera.transform.right * dir.x;
        playerMover.Move(dir * speed);
    }

    void LookForward()
    {
        Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
        latestPos = transform.position; //前回のPositionの更新
        diff = Vector3.Scale(diff, new Vector3(1, 0, 1)).normalized;
        if (diff.magnitude > 0.5f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
        }
    }
}