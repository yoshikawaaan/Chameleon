using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover
{
    private readonly Rigidbody _rigidbody;

    public PlayerMover(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
        
    }
    
    public void Move(Vector3 moveDirection)
    {
        _rigidbody.velocity = Time.deltaTime * moveDirection;
        
    }
}
