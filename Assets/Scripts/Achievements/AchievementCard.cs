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

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void SetupAchievement(Achievement achievement)
	{
		_achievementImage.sprite = achievement.Sprite;
		_titleText.text = achievement.Title;
		_progressText.text = $"0/{achievement.ProgressToUnlock}";
		_rewardText.text = achievement.GoldReward.ToString();
	}
	#endregion

	#region Private Methods


	#endregion
}
