using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	#region Fields & Properties

	[Header("Panels")]
	[SerializeField] GameObject _turretShopPanel;
	[SerializeField] GameObject _nodeUIPanel;

	[Header("Texts")]
	[SerializeField] TextMeshProUGUI _upgradeCostText;
	[SerializeField] TextMeshProUGUI _sellPriceText;
	[SerializeField] TextMeshProUGUI _turretLevelText;

	Node _currentSelectedNode;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void OnEnable() 
	{
		Node.OnNodeSelected += NodeSelected;
	}
	
	void OnDisable() 
	{
		Node.OnNodeSelected -= NodeSelected;
	}
	#endregion

	#region Public Methods

	public void CloseTurretShopPanel()
	{
		_turretShopPanel.SetActive(false);
	}

	public void UpgradeTurret()
	{
		_currentSelectedNode.Turret.TurretUpgrade.UpgradeTurret();
		UpdateUpgradeText();
		UpdateTurretLevelText();
	}
	#endregion

	#region Private Methods

	void NodeSelected(Node selectedNode)
	{
		_currentSelectedNode = selectedNode;
		if (_currentSelectedNode.IsEmpty())
		{
			ShowTurretShopPanel();
		}
		else
		{
			ShowNodeUI();
		}
	}

	void ShowTurretShopPanel()
	{
		_turretShopPanel.SetActive(true);
	}

	void ShowNodeUI()
	{
		_nodeUIPanel.SetActive(true);
		UpdateUpgradeText();
		UpdateTurretLevelText();
	}

	void UpdateUpgradeText()
	{
		_upgradeCostText.text = _currentSelectedNode.Turret.TurretUpgrade.UpgradeCost.ToString();
	}

	void UpdateTurretLevelText()
	{
		_turretLevelText.text = $"Level: {_currentSelectedNode.Turret.TurretUpgrade.Level}";
	}
	#endregion
}
