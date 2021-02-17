using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
	#region Fields & Properties

	Animator _anim;
	Enemy _enemy;
	EnemyHealth _enemyHealth;
	#endregion

	#region Getters


	#endregion

	#region Unity Methods

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
		_anim = GetComponent<Animator>();
		_enemy = GetComponent<Enemy>();
		_enemyHealth = GetComponent<EnemyHealth>();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	float GetCurrentAnimationLength()
	{
		return _anim.GetCurrentAnimatorStateInfo(0).length + 0.2f;
	}

	IEnumerator PlayHurtRoutine()
	{
		_enemy.StopMovement();
		PlayHurtAnimation();
		yield return new WaitForSeconds(GetCurrentAnimationLength());
		_enemy.ResumeMovement();
	}

	IEnumerator PlayDieRoutine()
	{
		_enemy.StopMovement();
		PlayDieAnimation();
		yield return new WaitForSeconds(GetCurrentAnimationLength());
		_enemy.ResumeMovement();
		_enemyHealth.ResetHealth();
		ObjectPooler.ReturnToPool(_enemy.gameObject);
	}

	void PlayHurtAnimation()
	{
		_anim.SetTrigger("Hurt");
	}

	void PlayDieAnimation()
	{
		_anim.SetTrigger("Die");
	}

	void EnemyHit(Enemy target)
	{
		if (_enemy == target)
		{
			StartCoroutine(PlayHurtRoutine());
		}
	}

	void EnemyDead(Enemy target)
	{
		if (_enemy == target)
		{
			StartCoroutine(PlayDieRoutine());
		}
	}
	#endregion
}
