using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter
{
	private float _maxHealthPoint;
	private float _healthPoint;

	
	protected void SetMaxHealthPoint(float maxHealth)
	{
		_maxHealthPoint = maxHealth;
	}
	protected void SetHealthPoint(float health)
	{
		_healthPoint = health;
		if (_healthPoint > _maxHealthPoint)
			_healthPoint = _maxHealthPoint;
	}
	protected void CalculateHealthPoint(float delta)
	{
		_healthPoint += delta;
	}
}