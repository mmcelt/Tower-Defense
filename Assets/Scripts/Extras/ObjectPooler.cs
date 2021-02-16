using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] GameObject _prefab;
	[SerializeField] int _poolSize = 10;

	List<GameObject> _pool;
	GameObject _poolContainer;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Awake() 
	{
		_pool = new List<GameObject>();
		_poolContainer = new GameObject($"Pool - {_prefab.name}");

		CreatePooler();
	}
	#endregion

	#region Public Methods

	public GameObject GetInstanceFromPool()
	{
		for (int i = 0; i < _pool.Count; i++)
		{
			if (!_pool[i].activeInHierarchy)
				return _pool[i];
		}

		return CreateInstance();
	}
	#endregion

	#region Private Methods

	void CreatePooler()
	{
		for (int i = 0; i < _poolSize; i++)
		{
			_pool.Add(CreateInstance());
		}
	}

	GameObject CreateInstance()
	{
		GameObject newInstance = Instantiate(_prefab, _poolContainer.transform);
		newInstance.SetActive(false);
		return newInstance;
	}
	#endregion
}
