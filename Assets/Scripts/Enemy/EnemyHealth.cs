using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	#region Fields & Properties

	public static Action<Enemy> OnEnemyKilled;
	public static Action<Enemy> OnEnemyHit;

	[SerializeField] GameObject _healthBarPrefab;
	[SerializeField] Transform _barPosition;
	[SerializeField] float _initialHealth, _maxHealth;

	public float CurrentHealth { get; set; }

	Image _healthbar;
	Enemy _enemy;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_enemy = GetComponent<Enemy>();
		CreateHealthBar();
		CurrentHealth = _initialHealth;
		UpdateHealthbar();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
			DealDamage(5f);

		UpdateHealthbar();
	}
	#endregion

	#region Public Methods

	public void DealDamage(float damageReceived)
	{
		CurrentHealth = Mathf.Max(CurrentHealth - damageReceived, 0);
		if (CurrentHealth == 0)
		{
			Die();
		}
		else
		{
			OnEnemyHit?.Invoke(_enemy);
		}
	}

	public void ResetHealth()
	{
		CurrentHealth = _initialHealth;
		_healthbar.fillAmount = 1f;
	}
	#endregion

	#region Private Methods

	void CreateHealthBar()
	{
		GameObject newBar = Instantiate(_healthBarPrefab, _barPosition.position, Quaternion.identity);
		newBar.transform.SetParent(transform);

		EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
		_healthbar = container.FillAmountImage;
	}

	void UpdateHealthbar()
	{
		_healthbar.fillAmount = Mathf.Lerp(_healthbar.fillAmount, CurrentHealth / _maxHealth, Time.deltaTime * 10f);
	}

	void Die()
	{
		OnEnemyKilled?.Invoke(_enemy);
	}
	#endregion
}
