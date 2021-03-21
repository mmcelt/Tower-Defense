using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
	#region Fields & Properties

	public static Action<Achievement> OnAchievementUnlocked;
	public static Action<Achievement> OnProgressUpdated;

	[SerializeField] AchievementCard _achievementCardPrefab;
	[SerializeField] Transform _achievementPanelContainer;
	[SerializeField] Achievement[] _achievements;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		LoadAchievements();
	}
	#endregion

	#region Public Methods

	public void AddProgress(string achievementID, int amount)
	{
		Achievement achievementWanted = AchievementExists(achievementID);
		if (achievementWanted != null)
		{
			achievementWanted.AddProgress(amount);
		}
	}
	#endregion

	#region Private Methods

	void LoadAchievements()
	{
		for (int i = 0; i < _achievements.Length; i++)
		{
			AchievementCard card = Instantiate(_achievementCardPrefab, _achievementPanelContainer);
			card.transform.localScale = Vector3.one;
			card.SetupAchievement(_achievements[i]);
		}
	}

	Achievement AchievementExists(string achievementID)
	{
		for (int i = 0; i < _achievements.Length; i++)
		{
			if (_achievements[i].ID == achievementID)
			{
				return _achievements[i];
			}
		}
		return null;
	}

	void ResetAchievements()
	{
		//foreach(Achievement achievement in _achievements)

	}
	#endregion
}
