using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCard : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] Image _achievementImage;
	[SerializeField] TMP_Text _titleText;
	[SerializeField] TMP_Text _progressText;
	[SerializeField] TMP_Text _rewardText;

	public Achievement AchievementLoaded { get; set; }

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void OnEnable() 
	{
		AchievementManager.OnProgressUpdated += UpdateProgress;
	}
	
	void OnDisable() 
	{
		AchievementManager.OnProgressUpdated -= UpdateProgress;
	}
	#endregion

	#region Public Methods

	public void SetupAchievement(Achievement achievement)
	{
		AchievementLoaded = achievement;
		_achievementImage.sprite = achievement.Sprite;
		_titleText.text = achievement.Title;
		_progressText.text = achievement.GetProgress();
		_rewardText.text = achievement.GoldReward.ToString();
	}
	#endregion

	#region Private Methods

	void UpdateProgress(Achievement achievementWithProgress)
	{
		if (AchievementLoaded == achievementWithProgress)
		{
			_progressText.text = achievementWithProgress.GetProgress();
		}
	}
	#endregion
}
