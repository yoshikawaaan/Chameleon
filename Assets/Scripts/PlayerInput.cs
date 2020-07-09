using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput:IPlayerInput
{
    private float _horizontal;
    private float _vertical;

    public void Inputting()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    public Vector3 MoveDirection() => new Vector3(_horizontal, 0, _vertical).normalized;

    public bool IsCapture() => Input.GetMouseButtonDown(0);

}