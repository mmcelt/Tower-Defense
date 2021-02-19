using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
	Animator _animator;
	Enemy _enemy;
	EnemyHealth _enemyHealth;

	#region Unity Callbacks
	void OnEnable()
	{
		EnemyHealth.OnEnemyHit += EnemyHit;
		EnemyHealth.OnEnemyKilled += EnemyDead;
	}

	void OnDisable()
	{
		EnemyHealth.OnEnemyHit -= EnemyHit;
		EnemyHealth.OnEnemyKilled -= EnemyDead;
	}

	void Start()
	{
		_animator = GetComponent<Animator>();
		_enemy = GetComponent<Enemy>();
		_enemyHealth = GetComponent<EnemyHealth>();
	}
	#endregion

	void PlayHurtAnimation()
	{
		_animator.SetTrigger("Hurt");
	}

	void PlayDieAnimation()
	{
		_animator.SetTrigger("Die");
	}

	float GetCurrentAnimationLenght()
	{
		float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
		return animationLenght;
	}

	IEnumerator PlayHurt()
	{
		_enemy.StopMovement();
		PlayHurtAnimation();
		yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
		_enemy.ResumeMovement();
	}

	IEnumerator PlayDead()
	{
		_enemy.StopMovement();
		PlayDieAnimation();
		yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
		_enemy.ResumeMovement();
		_enemyHealth.ResetHealth();
		ObjectPooler.ReturnToPool(_enemy.gameObject);
	}

	void EnemyHit(Enemy enemy)
	{
		if (_enemy == enemy)
		{
			StartCoroutine(PlayHurt());
		}
	}

	void EnemyDead(Enemy enemy)
	{
		if (_enemy == enemy)
		{
			StartCoroutine(PlayDead());
		}
	}
}
