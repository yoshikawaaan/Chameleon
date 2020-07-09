using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    void Inputting();
    Vector3 MoveDirection();
    bool IsCapture();
}
