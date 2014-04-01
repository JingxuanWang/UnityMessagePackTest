using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System;

namespace GdiSystem.Editor
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(GameBehaviour), true)]
	public class CustomInspector : UnityEditor.Editor
	{
		private Vector2 buttonSize = new Vector2(50, 20);

		/// <summary>
		/// Default OnInspectorGUI function. It added a HelpHeader.
		/// </summary>
		public override void OnInspectorGUI()
		{
			DrawDefaultHelpHeader(target.GetType());
			DrawDefaultInspector();
		}

		/// <summary>
		/// Draws the help header for a Class. 
		/// It contains a button description. 
		/// If no class description is provided then do nothing.
		/// </summary>
		/// <param name="target">Target.</param>
		public void DrawDefaultHelpHeader(Type t)
		{
			string classKey = t.Name + "ClassHelp";

			bool classFold = EditorPrefs.GetBool(classKey, true);

			// if this class doesn't have class description
			// then we don't draw help button
			if (t.GetCustomAttributes(typeof(GdiSystem.CustomDescription), false).Length == 0)
			{
				return;
			}

			// class help button
			Rect virtualRect = GUILayoutUtility.GetRect(buttonSize.x, buttonSize.y);
			if (GUI.Button(new Rect(virtualRect.xMax - buttonSize.x, virtualRect.yMin, buttonSize.x, buttonSize.y), "Class?"))
			{
				classFold = !classFold;
				EditorPrefs.SetBool(classKey, classFold);
			}

			if (!classFold)
			{
				foreach (Attribute attr in t.GetCustomAttributes(false))
				{
					if (attr.GetType().ToString() == "GdiSystem.CustomDescription")
					{
						CustomDescription customDesc = attr as CustomDescription;
						EditorGUILayout.HelpBox(customDesc.Description, MessageType.Info);
						return;
					}
				}

				// Default Class Description
				EditorGUILayout.HelpBox("No description has been added to this class yet.", MessageType.Warning);
			}
		}
	}
}