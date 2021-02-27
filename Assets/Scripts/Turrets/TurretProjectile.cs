using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
	[SerializeField] protected Transform projectileSpawnPosition;
	[SerializeField] protected float delayBetweenAttacks = 2f;
	[SerializeField] protected float _damage = 2f;

	protected float _nextAttackTime;
	protected ObjectPooler _pooler;
	protected Turret _turret;
	protected Projectile _currentProjectileLoaded;

	public float DelayPerShot { get; set; }

	public float Damage { get; set; }

	void Start()
	{
		_turret = GetComponent<Turret>();
		_pooler = GetComponent<ObjectPooler>();
		Damage = _damage;
		DelayPerShot = delayBetweenAttacks;

		LoadProjectile();
	}

	protected virtual void Update()
	{
		if (IsTurretEmpty())
		{
			LoadProjectile();
		}

		if (Time.time > _nextAttackTime)
		{
			if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null &&
				 _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
			{
				_currentProjectileLoaded.transform.parent = null;
				_currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
			}

			_nextAttackTime = Time.time + DelayPerShot;
		}
	}

	protected virtual void LoadProjectile()
	{
		GameObject newInstance = _pooler.GetInstanceFromPool();
		newInstance.transform.localPosition = projectileSpawnPosition.position;
		newInstance.transform.SetParent(projectileSpawnPosition);

		_currentProjectileLoaded = newInstance.GetComponent<Projectile>();
		_currentProjectileLoaded.TurretOwner = this;
		_currentProjectileLoaded.ResetProjectile();
		_currentProjectileLoaded.Damage = Damage;
		newInstance.SetActive(true);
	}

	bool IsTurretEmpty()
	{
		return _currentProjectileLoaded == null;
	}

	public void ResetTurretProjectile()
	{
		_currentProjectileLoaded = null;
	}
}