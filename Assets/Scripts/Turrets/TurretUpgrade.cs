using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] int _initialUpgradeCost, _incrementalUpgradeCost;
	[SerializeField] float _damageIncremental;
	[SerializeField] float _delayReduction;

	[Header("Sell")]
	[Range(0,1)] [SerializeField] float _sellPercentage;

	TurretProjectile _turretProjectile;

	public float SellPercentage { get; set; }

	public int  UpgradeCost { get; set; }

	public int Level { get; set; }

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_turretProjectile = GetComponent<TurretProjectile>();
		UpgradeCost = _initialUpgradeCost;
		Level = 1;
		SellPercentage = _sellPercentage;
	}
	#endregion

	#region Public Methods

	public void UpgradeTurret()
	{
		if (CurrencyManager.Instance.TotalCoins >= UpgradeCost)
		{
			_turretProjectile.Damage += _damageIncremental;
			_turretProjectile.DelayPerShot -= _delayReduction;
			UpdateUpgrade();
		}
	}

	public int GetSellValue()
	{
		return Mathf.RoundToInt(UpgradeCost * SellPercentage);
	}
	#endregion

	#region Private Methods

	void UpdateUpgrade()
	{
		CurrencyManager.Instance.RemoveCoins(UpgradeCost);
		UpgradeCost += _incrementalUpgradeCost;
		Level++;
	}
	#endregion
}
