using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Shop Setting")]
public class TurretSettings : ScriptableObject
{
	#region Fields & Properties

	[SerializeField] GameObject _turretPrefab;
	[SerializeField] int _turretShopCost;
	[SerializeField] Sprite _turretShopSprite;

	#endregion
}
