using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnModes
{
	Fixed,
	Random
}

public class Spawner : MonoBehaviour
{
	public static Action OnWaveCompleted;

	[Header("Settings")]
	[SerializeField] SpawnModes spawnMode = SpawnModes.Fixed;
	[SerializeField] int enemyCount = 10;
	[SerializeField] float delayBtwWaves = 1f;

	[Header("Fixed Delay")]
	[SerializeField] float delayBtwSpawns;

	[Header("Random Delay")]
	[SerializeField] float minRandomDelay;
	[SerializeField] float maxRandomDelay;

	float _spawnTimer;
	int _enemiesSpawned;
	int _enemiesRamaining;

	ObjectPooler _pooler;
	Waypoint _waypoint;

	void Start()
	{
		_pooler = GetComponent<ObjectPooler>();
		_waypoint = GetComponent<Waypoint>();

		_enemiesRamaining = enemyCount;
	}

	void Update()
	{
		_spawnTimer -= Time.deltaTime;
		if (_spawnTimer < 0)
		{
			_spawnTimer = GetSpawnDelay();
			if (_enemiesSpawned < enemyCount)
			{
				_enemiesSpawned++;
				SpawnEnemy();
			}
		}
	}

	void SpawnEnemy()
	{
		GameObject newInstance = _pooler.GetInstanceFromPool();
		Enemy enemy = newInstance.GetComponent<Enemy>();
		enemy.Waypoint = _waypoint;
		enemy.ResetEnemy();

		enemy.transform.localPosition = transform.position;
		newInstance.SetActive(true);
	}

	float GetSpawnDelay()
	{
		float delay = 0f;
		if (spawnMode == SpawnModes.Fixed)
		{
			delay = delayBtwSpawns;
		}
		else
		{
			delay = GetRandomDelay();
		}

		return delay;
	}

	float GetRandomDelay()
	{
		float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
		return randomTimer;
	}

	IEnumerator NextWave()
	{
		yield return new WaitForSeconds(delayBtwWaves);
		_enemiesRamaining = enemyCount;
		_spawnTimer = 0f;
		_enemiesSpawned = 0;
	}

	void RecordEnemy(Enemy enemy)
	{
		_enemiesRamaining--;
		if (_enemiesRamaining <= 0)
		{
			OnWaveCompleted?.Invoke();
			StartCoroutine(NextWave());
		}
	}

	void OnEnable()
	{
		Enemy.OnEndReached += RecordEnemy;
		EnemyHealth.OnEnemyKilled += RecordEnemy;
	}

	void OnDisable()
	{
		Enemy.OnEndReached -= RecordEnemy;
		EnemyHealth.OnEnemyKilled -= RecordEnemy;
	}
}
