using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[SerializeField] GameObject prefab;
	[SerializeField] int poolSize = 10;

	List<GameObject> _pool;
	GameObject _poolContainer;

	void Awake()
	{
		_pool = new List<GameObject>();
		_poolContainer = new GameObject($"Pool - {prefab.name}");

		CreatePooler();
	}

	void CreatePooler()
	{
		for (int i = 0; i < poolSize; i++)
		{
			_pool.Add(CreateInstance());
		}
	}

	GameObject CreateInstance()
	{
		GameObject newInstance = Instantiate(prefab);
		newInstance.transform.SetParent(_poolContainer.transform);
		newInstance.SetActive(false);
		return newInstance;
	}

	public GameObject GetInstanceFromPool()
	{
		for (int i = 0; i < _pool.Count; i++)
		{
			if (!_pool[i].activeInHierarchy)
			{
				return _pool[i];
			}
		}

		return CreateInstance();
	}

	public static void ReturnToPool(GameObject instance)
	{
		instance.SetActive(false);
	}

	public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
	{
		yield return new WaitForSeconds(delay);
		instance.SetActive(false);
	}
}
