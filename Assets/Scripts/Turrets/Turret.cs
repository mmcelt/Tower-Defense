﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	[SerializeField] float attackRange = 3f;

	public Enemy CurrentEnemyTarget { get; set; }

	bool _gameStarted;
	List<Enemy> _enemies;

	void Start()
	{
		_gameStarted = true;
		_enemies = new List<Enemy>();
	}

	void Update()
	{
		GetCurrentEnemyTarget();
		RotateTowardsTarget();
	}

	void GetCurrentEnemyTarget()
	{
		if (_enemies.Count <= 0)
		{
			CurrentEnemyTarget = null;
			return;
		}

		CurrentEnemyTarget = _enemies[0];
	}

	void RotateTowardsTarget()
	{
		if (CurrentEnemyTarget == null)
		{
			return;
		}

		Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
		float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
		transform.Rotate(0f, 0f, angle);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy newEnemy = other.GetComponent<Enemy>();
			_enemies.Add(newEnemy);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy enemy = other.GetComponent<Enemy>();
			if (_enemies.Contains(enemy))
			{
				_enemies.Remove(enemy);
			}
		}
	}

	void OnDrawGizmos()
	{
		if (!_gameStarted)
		{
			GetComponent<CircleCollider2D>().radius = attackRange;
		}

		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
