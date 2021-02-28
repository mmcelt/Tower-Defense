using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
	#region Fields & Properties

	[SerializeField] int _coinTest;

	public int TotalCoins { get; set; }

	string CURRENCY_SAVE_KEY = "MYGAME_CURRENCY";

	#endregion

	#region Unity Callbacks

	void OnEnable()
	{
		EnemyHealth.OnEnemyKilled += OnAddCoins;
	}

	void OnDisable()
	{
		EnemyHealth.OnEnemyKilled -= OnAddCoins;
	}

	void Start()
	{
		PlayerPrefs.DeleteKey(CURRENCY_SAVE_KEY);
		LoadCoins();
	}
	#endregion

	#region Getters


	#endregion

	#region Public Methods

	public void AddCoins(int amount)
	{
		TotalCoins += amount;
		PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
		PlayerPrefs.Save();
	}

	public void RemoveCoins(int amount)
	{
		if (TotalCoins >= amount)
		{
			TotalCoins -= amount;
			PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
			PlayerPrefs.Save();
		}
	}
	#endregion

	#region Private Methods

	void LoadCoins()
	{
		TotalCoins = PlayerPrefs.GetInt(CURRENCY_SAVE_KEY, _coinTest);
	}

	void OnAddCoins(Enemy enemy)
	{
		AddCoins(1);
	}
	#endregion
}
