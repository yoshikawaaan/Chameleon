using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float healthPoint=100;
    [SerializeField] private float maxHealthPoint=100;

    private PlayerCharacter _playerCharacter;
    
    void Start()
    {
        _playerCharacter = new PlayerCharacter(maxHealthPoint, healthPoint);
        
    }

    void Update()
    {
        
    }
}
