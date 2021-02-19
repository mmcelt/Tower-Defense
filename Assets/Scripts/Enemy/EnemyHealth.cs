using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public static Action<Enemy> OnEnemyKilled;
	public static Action<Enemy> OnEnemyHit;

	[SerializeField] GameObject healthBarPrefab;
	[SerializeField] Transform barPosition;

	[SerializeField] float initialHealth = 10f;
	[SerializeField] float maxHealth = 10f;

	public float CurrentHealth { get; set; }

	Image _healthBar;
	Enemy _enemy;

	void Start()
	{
		CreateHealthBar();
		CurrentHealth = initialHealth;

		_enemy = GetComponent<Enemy>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			DealDamage(5f);
		}

		_healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount,
			 CurrentHealth / maxHealth, Time.deltaTime * 10f);
	}

	void CreateHealthBar()
	{
		GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
		newBar.transform.SetParent(transform);

		EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
		_healthBar = container.FillAmountImage;
	}

	public void DealDamage(float damageReceived)
	{
		CurrentHealth -= damageReceived;
		if (CurrentHealth <= 0)
		{
			CurrentHealth = 0;
			Die();
		}
		else
		{
			OnEnemyHit?.Invoke(_enemy);
		}
	}

	public void ResetHealth()
	{
		CurrentHealth = initialHealth;
		_healthBar.fillAmount = 1f;
	}

	void Die()
	{
		OnEnemyKilled?.Invoke(_enemy);
	}
}
