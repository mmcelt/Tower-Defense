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

	[Header("Poolers")]
	[SerializeField] ObjectPooler _enemyWave10Pooler;
	[SerializeField] ObjectPooler _enemyWave20Pooler;
	[SerializeField] ObjectPooler _enemyWave30Pooler;
	[SerializeField] ObjectPooler _enemyWave40Pooler;
	[SerializeField] ObjectPooler _enemyWave50Pooler;

	float _spawnTimer;
	int _enemiesSpawned;
	int _enemiesRamaining;

	Waypoint _waypoint;

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

	void Start()
	{
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
		GameObject newInstance = GetPooler().GetInstanceFromPool();
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

	ObjectPooler GetPooler()
	{
		int currentWave = LevelManager.Instance.CurrentWave;
		if (currentWave <= 10)									//1-10
			return _enemyWave10Pooler;
		else if (currentWave > 10 && currentWave <= 20)	//11-20
			return _enemyWave20Pooler;
		else if (currentWave > 20 && currentWave <= 30)	//21-30
			return _enemyWave30Pooler;
		else if (currentWave > 30 && currentWave <= 40)	//31-40
			return _enemyWave40Pooler;
		else
			return _enemyWave50Pooler;							//>40
	}
}
