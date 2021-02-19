using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public static Action<Enemy> OnEndReached;

	[SerializeField] float moveSpeed = 3f;

	/// <summary>
	/// Move speed of our enemy
	/// </summary>
	public float MoveSpeed { get; set; }

	/// <summary>
	/// The waypoint reference
	/// </summary>
	public Waypoint Waypoint { get; set; }

	public EnemyHealth EnemyHealth { get; set; }

	/// <summary>
	/// Returns the current Point Position where this enemy needs to go
	/// </summary>
	public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

	int _currentWaypointIndex;
	Vector3 _lastPointPosition;

	EnemyHealth _enemyHealth;
	SpriteRenderer _spriteRenderer;

	void Start()
	{
		_enemyHealth = GetComponent<EnemyHealth>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		EnemyHealth = GetComponent<EnemyHealth>();

		_currentWaypointIndex = 0;
		MoveSpeed = moveSpeed;
		_lastPointPosition = transform.position;
	}

	void Update()
	{
		Move();
		Rotate();

		if (CurrentPointPositionReached())
		{
			UpdateCurrentPointIndex();
		}
	}

	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position,
			 CurrentPointPosition, MoveSpeed * Time.deltaTime);
	}

	public void StopMovement()
	{
		MoveSpeed = 0f;
	}

	public void ResumeMovement()
	{
		MoveSpeed = moveSpeed;
	}

	void Rotate()
	{
		if (CurrentPointPosition.x > _lastPointPosition.x)
		{
			_spriteRenderer.flipX = false;
		}
		else
		{
			_spriteRenderer.flipX = true;
		}
	}

	bool CurrentPointPositionReached()
	{
		float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
		if (distanceToNextPointPosition < 0.1f)
		{
			_lastPointPosition = transform.position;
			return true;
		}

		return false;
	}

	void UpdateCurrentPointIndex()
	{
		int lastWaypointIndex = Waypoint.Points.Length - 1;
		if (_currentWaypointIndex < lastWaypointIndex)
		{
			_currentWaypointIndex++;
		}
		else
		{
			EndPointReached();
		}
	}

	void EndPointReached()
	{
		OnEndReached?.Invoke(this);
		_enemyHealth.ResetHealth();
		ObjectPooler.ReturnToPool(gameObject);
	}

	public void ResetEnemy()
	{
		_currentWaypointIndex = 0;
	}
}
