using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
	public static DamageTextManager Instance;

	public ObjectPooler Pooler { get; set; }

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}

	void Start()
	{
		Pooler = GetComponent<ObjectPooler>();
	}
}
