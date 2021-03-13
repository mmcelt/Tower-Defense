using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	#region Fields & Properties

	public static Action<Node> OnNodeSelected;
	public static Action OnTurretSold;

	[SerializeField] GameObject _attackRangeSprite;

	public Turret Turret { get; set; }

	float _rangeSize;
	Vector3 _rangeOrginalSize;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_rangeSize = _attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
		_rangeOrginalSize = _attackRangeSprite.transform.localScale;
	}
	#endregion

	#region Public Methods

	public void SetTurret(Turret turret)
	{
		Turret = turret;
	}

	public bool IsEmpty()
	{
		return Turret == null;
	}

	public void SelectTurret()
	{
		OnNodeSelected?.Invoke(this);

		if (!IsEmpty())
		{
			ShowTurretInfo();
		}
	}

	public void SellTurret()
	{
		if (!IsEmpty())
		{
			CurrencyManager.Instance.AddCoins(Turret.TurretUpgrade.GetSellValue());
			Destroy(Turret.gameObject);
			Turret = null;
			CloseAttackRangeSprite();
			OnTurretSold?.Invoke();
		}
	}

	public void CloseAttackRangeSprite()
	{
		_attackRangeSprite.SetActive(false);
	}
	#endregion

	#region Private Methods

	void ShowTurretInfo()
	{
		_attackRangeSprite.SetActive(true);
		_attackRangeSprite.transform.localScale = _rangeOrginalSize * Turret.AttackRange / (_rangeSize / 2);
	}
	#endregion
}
