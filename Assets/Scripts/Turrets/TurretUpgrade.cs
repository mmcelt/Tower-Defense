using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] int _initialUpgradeCost, _incrementalUpgradeCost;
	[SerializeField] float _damageIncremental;
	[SerializeField] float _delayReduction;

	TurretProjectile _turretProjectile;

	public int  UpgradeCost { get; set; }

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_turretProjectile = GetComponent<TurretProjectile>();
		UpgradeCost = _initialUpgradeCost;
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
		if (CurrencyManager.Instance.TotalCoins >= UpgradeCost)
		{
			_turretProjectile.Damage += _damageIncremental;
			_turretProjectile.DelayPerShot -= _delayReduction;
			UpdateUpgrade();
		}
	}

	void UpdateUpgrade()
	{
		CurrencyManager.Instance.RemoveCoins(UpgradeCost);
		UpgradeCost += _incrementalUpgradeCost;
	}
	#endregion
}
