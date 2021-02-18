using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] Transform _projectileSpawnPosition;

	ObjectPooler _pooler;
	Projectile _currentProjectileLoaded;
	Turret _turret;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_pooler = GetComponent<ObjectPooler>();
		_turret = GetComponent<Turret>();
	}
	
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			LoadProjectile();
		}

		if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
		{
			_currentProjectileLoaded.transform.parent = null;
			_currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void LoadProjectile()
	{
		GameObject newInstance = _pooler.GetInstanceFromPool();
		newInstance.transform.position = _projectileSpawnPosition.position;
		newInstance.transform.SetParent(_projectileSpawnPosition);
		_currentProjectileLoaded = newInstance.GetComponent<Projectile>();
		newInstance.SetActive(true);
	}
	#endregion
}
