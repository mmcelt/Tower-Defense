using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretCard : MonoBehaviour
{
	#region Fields & Properties

	public static Action<TurretSettings> OnPlaceTurret;

	[SerializeField] Image _turretImage;
	[SerializeField] TextMeshProUGUI _turretCostText;

	public TurretSettings LoadedTurret { get; set; }

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void SetupTurretButton(TurretSettings turretSettings)
	{
		LoadedTurret = turretSettings;
		_turretImage.sprite = turretSettings._turretShopSprite;
		_turretCostText.text = turretSettings._turretShopCost.ToString();
	}

	public void PlaceTurret()
	{
		if (CurrencyManager.Instance.TotalCoins >= LoadedTurret._turretShopCost)
		{
			CurrencyManager.Instance.RemoveCoins(LoadedTurret._turretShopCost);
			UIManager.Instance.CloseTurretShopPanel();
			OnPlaceTurret?.Invoke(LoadedTurret);
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
