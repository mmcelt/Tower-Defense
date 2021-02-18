using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] float _attackRange = 3f;

	List<Enemy> _enemies;

	bool _gameStarted;

	public Enemy CurrentEnemyTarget { get; set; }

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

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
			if(_enemies.Contains(enemy))
				_enemies.Remove(enemy);
		}
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
		if (CurrentEnemyTarget == null) return;

		Vector3 targetPosition = CurrentEnemyTarget.transform.position - transform.position;
		float angle = Vector3.SignedAngle(transform.up, targetPosition, transform.forward);
		transform.Rotate(0f, 0f, angle);
	}
	#endregion
}
