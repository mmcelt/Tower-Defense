using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
	#region Fields & Properties

	Waypoint Waypoint => target as Waypoint;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void OnSceneGUI()
	{
		Handles.color = Color.red;

		for (int i = 0; i < Waypoint.Points.Length; i++)
		{
			EditorGUI.BeginChangeCheck();

			//create handles
			Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
			Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

			//create text labels
			GUIStyle textStyle = new GUIStyle();
			textStyle.fontStyle = FontStyle.Bold;
			textStyle.fontSize = 16;
			textStyle.normal.textColor = Color.yellow;
			Vector3 textAlignment = Vector3.down * 0.35f + Vector3.right * 0.35f;
			Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAlignment, text: $"{i + 1}", textStyle);

			EditorGUI.EndChangeCheck();

			//update waypoint position as it is dragged around
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, name: "Free Move Handle");
				Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
