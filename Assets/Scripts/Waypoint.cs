using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] Vector3[] _points;

	Vector3 _currentPosition;
	bool _gameStarted;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_gameStarted = true;
		_currentPosition = transform.position;
	}
	
	void Update() 
	{
		
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
				Gizmos.color = Color.yellow;
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
