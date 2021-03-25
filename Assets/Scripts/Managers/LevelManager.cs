using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] int lives = 10;

	public int TotalLives { get; set; }

	public int CurrentWave { get; set; }

	void OnEnable()
	{
		Enemy.OnEndReached += ReduceLives;
		Spawner.OnWaveCompleted += WaveCompleted;
	}

	void OnDisable()
	{
		Enemy.OnEndReached -= ReduceLives;
		Spawner.OnWaveCompleted -= WaveCompleted;
	}

	void Start()
	{
		TotalLives = lives;
		CurrentWave = 1;
		Time.timeScale = 1.0f;
	}

	void ReduceLives(Enemy enemy)
	{
		TotalLives--;
		if (TotalLives <= 0)
		{
			TotalLives = 0;
			// Game Over
			GameOver();
		}
	}

	void GameOver()
	{
		UIManager.Instance.ShowGameOverPanel();
	}

	void WaveCompleted()
	{
		CurrentWave++;
		AchievementManager.Instance.AddProgress("Waves10", 1);
		AchievementManager.Instance.AddProgress("Waves20", 1);
		AchievementManager.Instance.AddProgress("Waves50", 1);
		AchievementManager.Instance.AddProgress("Waves100", 1);
	}
}
