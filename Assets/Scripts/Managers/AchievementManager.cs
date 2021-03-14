using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	#region Fields & Properties

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
	#endregion
}
