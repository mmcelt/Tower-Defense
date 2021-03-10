using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	#region Fields & Properties

	[Header("Panels")]
	[SerializeField] GameObject _turretShopPanel;

	Node _currentSelectedNode;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void OnEnable() 
	{
		Node.OnNodeSelected += NodeSelected;
	}
	
	void OnDisable() 
	{
		Node.OnNodeSelected -= NodeSelected;
	}
	#endregion

	#region Public Methods

	public void CloseTurretShopPanel()
	{
		_turretShopPanel.SetActive(false);
	}
	#endregion

	#region Private Methods

	void NodeSelected(Node selectedNode)
	{
		_currentSelectedNode = selectedNode;
		if (_currentSelectedNode.IsEmpty())
		{
			_turretShopPanel.SetActive(true);
		}
	}
	#endregion
}
