using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Achievement")]
public class Achievement : ScriptableObject
{
	#region Fields & Properties

	public string ID;
	public string Title;
	public int ProgressToUnlock;
	public int GoldReward;
	public Sprite Sprite;

	public bool IsUnlocked { get; set; }

	int _currentProgress;

	#endregion

	#region Unity Callbacks

	void OnEnable()
	{
		IsUnlocked = false;
		_currentProgress = 0;
	}
	#endregion

	#region Public Methods

	public void AddProgress(int amount)
	{
		_currentProgress += amount;
		AchievementManager.OnProgressUpdated?.Invoke(this);
		CheckUnlockStatus();
	}

	public string GetProgress()
	{
		return $"{_currentProgress}/{ProgressToUnlock}";
	}

	public string GetProgressCompleted()
	{
		return $"{ProgressToUnlock}/{ProgressToUnlock}";
	}
	#endregion

	#region Private Methods

	void CheckUnlockStatus()
	{
		if (_currentProgress >= ProgressToUnlock)
		{
			UnlockAchievement();
		}
	}

	void UnlockAchievement()
	{
		IsUnlocked = true;
		AchievementManager.OnAchievementUnlocked?.Invoke(this);
	}
	#endregion
}
