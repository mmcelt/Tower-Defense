using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopManager : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] GameObject _turretButtonPrefab;
	[SerializeField] Transform _turretPanelContainer;

	[Header("Turret Settings")]
	[SerializeField] TurretSettings[] _turrets;


	Node _currentSelectedNode;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void OnEnable()
	{
		Node.OnNodeSelected += NodeSelected;
		Node.OnTurretSold += TurretSold;
		TurretCard.OnPlaceTurret += PlaceTurret;
	}

	void OnDisable()
	{
		Node.OnNodeSelected -= NodeSelected;
		Node.OnTurretSold -= TurretSold;
		TurretCard.OnPlaceTurret -= PlaceTurret;
	}

	void Start() 
	{
		for (int i = 0; i < _turrets.Length; i++)
		{
			CreateTurretButton(_turrets[i]);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void CreateTurretButton(TurretSettings turretSettings)
	{
		GameObject newInstance = Instantiate(_turretButtonPrefab, _turretPanelContainer);
		newInstance.transform.localScale = Vector3.one;

		TurretCard cardButton = newInstance.GetComponent<TurretCard>();
		cardButton.SetupTurretButton(turretSettings);
	}


	void NodeSelected(Node selectedNode)
	{
		_currentSelectedNode = selectedNode;
	}

	void TurretSold()
	{
		_currentSelectedNode = null;
	}

	void PlaceTurret(TurretSettings turretToPlace)
	{
		if (_currentSelectedNode != null)
		{
			GameObject turretInstance = Instantiate(turretToPlace._turretPrefab, _currentSelectedNode.transform);

			Turret turretPlaced = turretInstance.GetComponent<Turret>();
			_currentSelectedNode.SetTurret(turretPlaced);
		}
	}
	#endregion
}
