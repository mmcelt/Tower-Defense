using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	#region Fields & Properties

	public static Action<Enemy> OnEndReached;

	[SerializeField] float _moveSpeed = 3f;

	int _currentWaypointIndex;

	/// <summary>
	/// returns the current point position where this enemy needs to go
	/// </summary>
	public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

	public float MoveSpeed { get; set; }
	public Waypoint Waypoint { get; set; }

	EnemyHealth _enemyHealth;
	SpriteRenderer _sprite;

	Vector3 _lastPointPosition;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_enemyHealth = GetComponent<EnemyHealth>();
		_sprite = GetComponent<SpriteRenderer>();

		_currentWaypointIndex = 0;
		MoveSpeed = _moveSpeed;
		_lastPointPosition = transform.position;
	}
	
	void Update() 
	{
		Move();
		Rotate();

		if (CurrentPositionReached())
			UpdateCurrentPointIndex();
	}
	#endregion

	#region Public Methods

	public void ResetEnemy()
	{
		_currentWaypointIndex = 0;
	}

	public void StopMovement()
	{
		MoveSpeed = 0f;
	}

	public void ResumeMovement()
	{
		MoveSpeed = _moveSpeed;
	}
	#endregion

	#region Private Methods

	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
	}

	void Rotate()
	{
		if(CurrentPointPosition.x > _lastPointPosition.x)	//moving right
		{
			_sprite.flipX = false;
		}
		else
		{
			_sprite.flipX = true;
		}
	}

	bool CurrentPositionReached()
	{
		float distToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
		if (distToNextPointPosition < 0.1f)
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
			_currentWaypointIndex++;
		else
		{
			EndpointReached();
		}
	}
	void EndpointReached()
	{
		OnEndReached?.Invoke(this); //check for listeners, if there are some-fire the event
		_enemyHealth.ResetHealth();
		ObjectPooler.ReturnToPool(gameObject);
	}
	#endregion
}
