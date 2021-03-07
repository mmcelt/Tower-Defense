using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretCard : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] Image _turretImage;
	[SerializeField] TextMeshProUGUI _turretCostText;

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
		_turretImage.sprite = turretSettings._turretShopSprite;
		_turretCostText.text = turretSettings._turretShopCost.ToString();
	}
	#endregion

	#region Private Methods


	#endregion
}
