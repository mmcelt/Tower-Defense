using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	#region Fields & Properties

	public static Action OnEndReached;

	[SerializeField] float _moveSpeed = 3f;
	[SerializeField] Waypoint _waypoint;

	int _currentWaypointIndex;

	public Vector3 CurrentPointPosition => _waypoint.GetWaypointPosition(_currentWaypointIndex);

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_currentWaypointIndex = 0;
	}
	
	void Update() 
	{
		Move();
		if (CurrentPositionReached())
			UpdateCurrentPointIndex();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, _moveSpeed * Time.deltaTime);
	}

	bool CurrentPositionReached()
	{
		float distToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
		if (distToNextPointPosition < 0.1f)
			return true;

		return false;
	}

	void UpdateCurrentPointIndex()
	{
		int lastWaypointIndex = _waypoint.Points.Length - 1;
		if (_currentWaypointIndex < lastWaypointIndex)
			_currentWaypointIndex++;
		else
		{
			ReturnEnemyToPool();
		}
	}
	void ReturnEnemyToPool()
	{
		OnEndReached?.Invoke();	//check for listeners, if there are some-fire the event
		ObjectPooler.ReturnToPool(gameObject);
	}
	#endregion
}