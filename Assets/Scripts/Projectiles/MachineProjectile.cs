﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineProjectile : Projectile
{
	public Vector2 Direction { get; set; }

	
	void OnEnable()
	{
		StartCoroutine(ObjectPooler.ReturnToPoolWithDelay(gameObject, 0.3f));
	}

	protected override void Update()
	{
			MoveProjectile();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy enemy = other.GetComponent<Enemy>();
			if (enemy.EnemyHealth.CurrentHealth > 0)
			{
				OnEnemyHit?.Invoke(enemy, Damage);
				enemy.EnemyHealth.DealDamage(Damage);
			}
			ObjectPooler.ReturnToPool(gameObject);
		}
	}

	protected override void MoveProjectile()
	{
		Vector2 movement = Direction.normalized * moveSpeed * Time.deltaTime;
		transform.Translate(movement);
	}
}
