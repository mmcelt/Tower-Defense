using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] Vector3[] _points;

	Vector3 _currentPosition;
	bool _gameStarted;

	public Vector3[] Points => _points;
	public Vector3 CurrentPosition => _currentPosition;

	#endregion

	#region Getters

	public Vector3 GetWaypointPosition(int index)
	{
		return CurrentPosition + Points[index];
	}
	#endregion

	#region Unity Methods

	void Start() 
	{
		_gameStarted = true;
		_currentPosition = transform.position;
	}

	void OnDrawGizmos()
	{
		if (!_gameStarted && transform.hasChanged)
		{
			_currentPosition = transform.position;
		}

		for (int i = 0; i < _points.Length; i++)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(_points[i] + _currentPosition, 0.5f);

			if (i <_points.Length-1)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawLine(_points[i] + _currentPosition, _points[i + 1] + _currentPosition);
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
