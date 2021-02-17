using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] int _lives = 10;

	public int TotalLives { get; private set; }

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void OnEnable()
	{
		Enemy.OnEndReached += ReduceLives;
	}

	void OnDisable()
	{
		Enemy.OnEndReached -= ReduceLives;
	}

	void Start() 
	{
		TotalLives = _lives;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void ReduceLives(Enemy target)
	{
		TotalLives = Mathf.Max(TotalLives - 1, 0);
		if (TotalLives == 0)
		{
			//game over logic
		}
	}
	#endregion
}
