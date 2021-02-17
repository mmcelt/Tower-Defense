using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] float _attackRange = 3f;

	bool _gameStarted;
	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_gameStarted = true;
	}
	
	void Update() 
	{
		
	}

	void OnDrawGizmos()
	{
		if (!_gameStarted)
		{
			GetComponent<CircleCollider2D>().radius = _attackRange;
		}

		Gizmos.DrawWireSphere(transform.position, _attackRange);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
