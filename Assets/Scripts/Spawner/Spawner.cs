using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
	Fixed,
	Random
}

public class Spawner : MonoBehaviour
{
	#region Fields & Properties

	[Header("General Settings")]
	[SerializeField] SpawnModes _spawnMode = SpawnModes.Fixed;
	[SerializeField] int _enemyCount = 10;
	[SerializeField] float _delayBetweenWaves = 1f;
	[Header("Fixed Spawner")]
	[SerializeField] float _delayBetweenSpawns;
	[Header("Random Spawner")]
	[SerializeField] float _minRandomDelay;
	[SerializeField] float _maxRandomDelay;

	float _spawnTimer;
	int _enemiesSpawned;
	int _enemiesRemaining;

	ObjectPooler _pooler;
	Waypoint _waypoint;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

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
		_pooler = GetComponent<ObjectPooler>();
		_waypoint = GetComponent<Waypoint>();
		_enemiesRemaining = _enemyCount;
	}
	
	void Update() 
	{
		_spawnTimer -= Time.deltaTime;

		if (_spawnTimer <= 0)
		{
			_spawnTimer = GetSpawnDelay();

			if (_enemiesSpawned < _enemyCount)
			{
				_enemiesSpawned++;
				SpawnEnemy();
			}
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

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

		if (_spawnMode == SpawnModes.Fixed)
			delay = _delayBetweenSpawns;
		else
			delay = GetRandomDelay();

		return delay;
	}

	float GetRandomDelay()
	{
		float randomTimer = Random.Range(_minRandomDelay, _maxRandomDelay);

		return randomTimer;
	}

	void RecordEnemy(Enemy target)
	{
		_enemiesRemaining = Mathf.Max(_enemiesRemaining - 1, 0);
		if (_enemiesRemaining == 0)
		{
			StartCoroutine(NextWaveRoutine());
		}
	}

	IEnumerator NextWaveRoutine()
	{
		yield return new WaitForSeconds(_delayBetweenWaves);
		_enemiesRemaining = _enemyCount;
		_spawnTimer = 0f;
		_enemiesSpawned = 0;
	}
	#endregion
}
