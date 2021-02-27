using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] int _upgradeInitialCost, _upgradeCostIncremental;
	[SerializeField] float _damageIncremental;
	[SerializeField] float _delayReduction;

	TurretProjectile _turretProjectile;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_turretProjectile = GetComponent<TurretProjectile>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.D))
			UpgradeTurret();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void UpgradeTurret()
	{
		_turretProjectile.Damage += _damageIncremental;
		_turretProjectile.DelayPerShot -= _delayReduction;
	}
	#endregion
}
