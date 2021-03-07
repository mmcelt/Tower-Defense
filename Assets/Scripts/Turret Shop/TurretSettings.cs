using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Shop Setting")]
public class TurretSettings : ScriptableObject
{
	#region Fields & Properties

	public GameObject _turretPrefab;
	public int _turretShopCost;
	public Sprite _turretShopSprite;

	#endregion
}
