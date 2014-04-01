using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;

namespace GdiSystem.Editor
{
	public class CustomInspectorUtility
	{
		static private Vector2 buttonSize = new Vector2(50, 20);

		/// <summary>
		/// Draws the help header for a Class. It contains a button and class description
		/// </summary>
		/// <param name="target">Target.</param>
		static public void DrawHelpHeader(Type t)
		{
			string classKey = t.Name + "ClassHelp";
			string fieldKey = t.Name + "FieldHelp";

			bool classFold = EditorPrefs.GetBool(classKey, true);
			bool fieldFold = EditorPrefs.GetBool(fieldKey, true);

			// class help button
			Rect virtualRect = GUILayoutUtility.GetRect(buttonSize.x, buttonSize.y);
			if (GUI.Button(new Rect(virtualRect.xMax - buttonSize.x * 2.1f, virtualRect.yMin, buttonSize.x, buttonSize.y), "Class?"))
			{
				classFold = !classFold;
				EditorPrefs.SetBool(classKey, classFold);
			}

			// field help button
			if (GUI.Button(new Rect(virtualRect.xMax - buttonSize.x, virtualRect.yMin, buttonSize.x, buttonSize.y), "Field?"))
			{
				fieldFold = !fieldFold;
				EditorPrefs.SetBool(fieldKey, fieldFold);
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

		/// <summary>
		/// Draws the help box by type.
		/// </summary>
		/// <param name="t">T.</param>
		/// <param name="key">Key.</param>
		/// <param name="property">Property.</param>
		static public void DrawHelpBox(Type t, SerializedProperty property, string initTypeName = "")
		{
			// ensure all recursive calls 
			// use the same initTypeName 
			// and the same fieldKey
			if (initTypeName == "")
			{
				initTypeName = t.Name;
			}

			string fieldKey = initTypeName + "FieldHelp";
			bool fieldFold = EditorPrefs.GetBool(fieldKey, true);

			if (!fieldFold)
			{
				//Debug.Log(t + " : " + t.BaseType);
				// find fields defined by current class
				foreach (FieldInfo field in t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
				{
					// stop if hit
					if (field.Name == property.name)
					{
						CustomDescription customDesc = 
							(CustomDescription)Attribute.GetCustomAttribute(
								field, typeof(CustomDescription), true
							);
						EditorGUILayout.HelpBox(customDesc.Description, MessageType.Info);
						//Debug.Log("hit : " + field.Name + " : " + property.name);
						return;
					}
				}

				// recursively find fields that defined by parent class
				Type basetype = t.BaseType;
				if (basetype != null && basetype != typeof(GdiSystem.GameBehaviour))
				{
					 DrawHelpBox(basetype, property, initTypeName);
				}
			}
		}

		/// <summary>
		/// Draws the help box.
		/// </summary>
		/// <param name="t">T.</param>
		/// <param name="description">Description.</param>
		static public void DrawHelpBox(Type t, string description)
		{
			string fieldKey = t.Name + "FieldHelp";
			bool fieldFold = EditorPrefs.GetBool(fieldKey, true);

			if (!fieldFold)
			{
				EditorGUILayout.HelpBox(description, MessageType.Info);
			}
		}

		static public void SetLabelWidth(float width)
		{
			#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2
			EditorGUIUtility.LookLikeControls(width);
			#else
			EditorGUIUtility.labelWidth = width;
			#endif
		}

		/// <summary>
		/// Draw a distinctly different looking header label
		/// </summary>

		static public bool DrawHeader(string text)
		{
			return DrawHeader(text, text, false);
		}

		/// <summary>
		/// Draw a distinctly different looking header label
		/// </summary>

		static public bool DrawHeader(string text, string key)
		{
			return DrawHeader(text, key, false);
		}

		/// <summary>
		/// Draw a distinctly different looking header label
		/// </summary>

		static public bool DrawHeader(string text, bool forceOn)
		{
			return DrawHeader(text, text, forceOn);
		}

		/// <summary>
		/// Draw a distinctly different looking header label
		/// </summary>

		static public bool DrawHeader(string text, string key, bool forceOn)
		{
			// レジストリから取得する
			bool state = EditorPrefs.GetBool(key, true);

			GUILayout.Space(3f);
			if (!forceOn && !state)
				GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
			GUILayout.BeginHorizontal();
			GUILayout.Space(3f);

			GUI.changed = false;
			#if UNITY_3_5
			if (state) text = "\u25B2 " + text;
			else text = "\u25BC " + text;
			if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
			#else
			text = "<b><size=11>" + text + "</size></b>";
			if (state)
				text = "\u25B2 " + text;
			else
				text = "\u25BC " + text;
			if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f)))
				state = !state;
			#endif
			if (GUI.changed)
				EditorPrefs.SetBool(key, state);

			GUILayout.Space(2f);
			GUILayout.EndHorizontal();
			GUI.backgroundColor = Color.white;
			if (!forceOn && !state)
				GUILayout.Space(3f);
			return state;
		}

		/// <summary>
		/// Begin drawing the content area.
		/// </summary>

		static public void BeginContents()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(4f);
			EditorGUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
			GUILayout.BeginVertical();
			GUILayout.Space(2f);
		}

		/// <summary>
		/// End drawing the content area.
		/// </summary>

		static public void EndContents()
		{
			GUILayout.Space(3f);
			GUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
			GUILayout.Space(3f);
			GUILayout.EndHorizontal();
			GUILayout.Space(3f);
		}

		public static void FieldSelection(SerializedProperty p, Type componentType, string propertyName)
		{
			GameBehaviour behaviour = p.serializedObject.targetObject as GameBehaviour;
			Component c = behaviour.GetComponent(componentType);
			HashSet<string> h = componentType.InvokeMember(propertyName, BindingFlags.GetProperty |
				BindingFlags.Instance | BindingFlags.Public,
				null, c, null) as HashSet<string>;
			List<string> displayed = h.ToList();

			if (displayed.Count < 1)
			{
				EditorGUILayout.HelpBox("Selection Provider is Empty !", MessageType.Warning);
			}
			else
			{
				string currentValue = p.stringValue;
				int index = displayed.FindIndex(t => t == currentValue);
				if (index < 0)
				{
					index = 0;
				}
				index = EditorGUILayout.Popup(new GUIContent(p.name),
					index, displayed.ConvertAll(t => new GUIContent(t)).ToArray());
				p.stringValue = displayed[index];
			}
		}

		static public void CheckRequired(SerializedProperty serializedGameObject, Type requireType)
		{
			GameObject oldObj = serializedGameObject.objectReferenceValue as GameObject;
			GameObject newObj = EditorGUILayout.ObjectField(
				new GUIContent(requireType.Name), oldObj, typeof(GameObject), true) as GameObject;

			// 正常な値の場合は更新する
			if (newObj == null || (newObj != null && newObj.GetComponent(requireType) != null))
			{
				serializedGameObject.objectReferenceValue = newObj;
			}

			// 異常な値の場合は
			if (newObj == null || newObj.GetComponent(requireType) == null)
			{
				EditorGUILayout.HelpBox("Required Component [" + requireType.Name + "]", MessageType.Warning);
			}
		}

		static public void CheckRequired(SerializedProperty serializedGameObject, Type requireType, Type objType, string description)
		{
			GameObject oldObj = serializedGameObject.objectReferenceValue as GameObject;
			GameObject newObj = EditorGUILayout.ObjectField(
				new GUIContent(requireType.Name), oldObj, typeof(GameObject), true) as GameObject;

			// 正常な値の場合は更新する
			if (newObj == null || (newObj != null && newObj.GetComponent(requireType) != null))
			{
				serializedGameObject.objectReferenceValue = newObj;
			}

			// 異常な値の場合は
			if (newObj == null || newObj.GetComponent(requireType) == null)
			{
				EditorGUILayout.HelpBox("Required Component [" + requireType.Name + "]", MessageType.Warning);
			}

			// if description is defined
			DrawHelpBox(objType, description);
		}
	}
}