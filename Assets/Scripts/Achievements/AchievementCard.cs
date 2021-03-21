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
	[SerializeField] Button _rewardButton;

	public Achievement AchievementLoaded { get; set; }

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void OnEnable() 
	{
		CheckRewardButtonStatus();
		LoadAchievementProgress();
		AchievementManager.OnProgressUpdated += UpdateProgress;
		AchievementManager.OnAchievementUnlocked += AchievementUnlocked;
	}
	
	void OnDisable() 
	{
		AchievementManager.OnProgressUpdated -= UpdateProgress;
		AchievementManager.OnAchievementUnlocked -= AchievementUnlocked;
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

	public void GetReward()
	{
		if (AchievementLoaded.IsUnlocked)
		{
			CurrencyManager.Instance.AddCoins(AchievementLoaded.GoldReward);
			_rewardButton.gameObject.SetActive(false);
		}
	}
	#endregion

	#region Private Methods

	void UpdateProgress(Achievement achievementWithProgress)
	{
		if (AchievementLoaded == achievementWithProgress)
		{
			LoadAchievementProgress();
		}
	}
	void AchievementUnlocked(Achievement achievementUnlocked)
	{
		if (AchievementLoaded == achievementUnlocked)
		{
			CheckRewardButtonStatus();
		}
	}

	void CheckRewardButtonStatus()
	{
		if (AchievementLoaded.IsUnlocked)
			_rewardButton.interactable = true;
		else
			_rewardButton.interactable = false;
	}

	void LoadAchievementProgress()
	{
		if (AchievementLoaded.IsUnlocked)
			_progressText.text = AchievementLoaded.GetProgressCompleted();
		else
			_progressText.text = AchievementLoaded.GetProgress();
	}
	#endregion
}
