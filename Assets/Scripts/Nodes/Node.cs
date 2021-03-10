using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	#region Fields & Properties

	public static Action<Node> OnNodeSelected;

	public Turret Turret { get; set; }

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

	public void SetTurret(Turret turret)
	{
		Turret = turret;
	}

	public bool IsEmpty()
	{
		return Turret == null;
	}

	public void SelectTurret()
	{
		OnNodeSelected?.Invoke(this);
	}
	#endregion

	#region Private Methods


	#endregion
}
