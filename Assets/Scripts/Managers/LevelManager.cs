using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] int lives = 10;

	public int TotalLives { get; set; }

	void Start()
	{
		TotalLives = lives;
	}

	void ReduceLives(Enemy enemy)
	{
		TotalLives--;
		if (TotalLives <= 0)
		{
			TotalLives = 0;
			// Game Over
		}
	}

	void OnEnable()
	{
		Enemy.OnEndReached += ReduceLives;
	}

	void OnDisable()
	{
		Enemy.OnEndReached -= ReduceLives;
	}
}
