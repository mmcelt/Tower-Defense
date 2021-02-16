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
	[Header("Fixed Spawner")]
	[SerializeField] float _delayBetweenSpawns;
	[Header("Random Spawner")]
	[SerializeField] float _minRandomDelay;
	[SerializeField] float _maxRandomDelay;

	float _spawnTimer;
	int _enemiesSpawned;

	ObjectPooler _pooler;
	Waypoint _waypoint;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_pooler = GetComponent<ObjectPooler>();
		_waypoint = GetComponent<Waypoint>();
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
	#endregion
}
