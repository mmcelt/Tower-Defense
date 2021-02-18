using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] float _moveSpeed = 10f;

	Enemy _enemyTarget;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_enemyTarget != null)
		{
			MoveProjectile();
			RotateProjectile();
		}
	}
	#endregion

	#region Public Methods

	public void SetEnemy(Enemy target)
	{
		_enemyTarget = target;
	}
	#endregion

	#region Private Methods

	void MoveProjectile()
	{
		transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, _moveSpeed * Time.deltaTime);
	}

	void RotateProjectile()
	{
		Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
		float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
		transform.Rotate(0f, 0f, angle);
	}
	#endregion
}
