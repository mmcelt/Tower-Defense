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
	[SerializeField] TMP_Text _livesText;
	[SerializeField] TMP_Text _coinsText;
	[SerializeField] TMP_Text _currentWaveText;

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

	void Update()
	{
		_coinsText.text = CurrencyManager.Instance.TotalCoins.ToString();
		_livesText.text = LevelManager.Instance.TotalLives.ToString();
		_currentWaveText.text = $"Wave: {LevelManager.Instance.CurrentWave}";
	}
	#endregion

	#region Public Methods

	public void CloseTurretShopPanel()
	{
		_turretShopPanel.SetActive(false);
	}

	public void CloseNodeUIPanel()
	{
		_currentSelectedNode.CloseAttackRangeSprite();
		_nodeUIPanel.SetActive(false);
	}

	public void UpgradeTurret()
	{
		_currentSelectedNode.Turret.TurretUpgrade.UpgradeTurret();
		UpdateUpgradeText();
		UpdateTurretLevelText();
		UpdateSellValueText();
	}

	public void SellTurret()
	{
		_currentSelectedNode.SellTurret();
		_currentSelectedNode = null;
		_nodeUIPanel.SetActive(false);
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
		UpdateSellValueText();
	}

	void UpdateUpgradeText()
	{
		_upgradeCostText.text = _currentSelectedNode.Turret.TurretUpgrade.UpgradeCost.ToString();
	}

	void UpdateTurretLevelText()
	{
		_turretLevelText.text = $"Level: {_currentSelectedNode.Turret.TurretUpgrade.Level}";
	}

	void UpdateSellValueText()
	{
		int sellAmount = _currentSelectedNode.Turret.TurretUpgrade.GetSellValue();
		_sellPriceText.text = sellAmount.ToString();
	}
	#endregion
}
