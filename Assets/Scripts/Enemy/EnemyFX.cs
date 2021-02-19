using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
	[SerializeField] Transform textDamageSpawnPosition;

	Enemy _enemy;

	#region Unity Callbacks

	void OnEnable()
	{
		Projectile.OnEnemyHit += EnemyHit;
	}

	void OnDisable()
	{
		Projectile.OnEnemyHit -= EnemyHit;
	}

	void Start()
	{
		_enemy = GetComponent<Enemy>();
	}
	#endregion

	void EnemyHit(Enemy enemy, float damage)
	{
		if (_enemy == enemy)
		{
			GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
			TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
			damageText.text = damage.ToString();

			newInstance.transform.SetParent(textDamageSpawnPosition);
			newInstance.transform.position = textDamageSpawnPosition.position;
			newInstance.SetActive(true);
		}
	}
}
