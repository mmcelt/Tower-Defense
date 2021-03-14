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
	
	#endregion
}
