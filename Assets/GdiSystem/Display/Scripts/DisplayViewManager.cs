using System;
using UnityEngine;
using GdiSystem.Display.Internal;

namespace GdiSystem.Display
{
//	public interface IUnityObject
//	{
//		event DestroyHandler DestroyEvent;
//
//		int GetInstanceID();
//	}

	public delegate TValue Binder<TValue>();
	public class DisplayViewManager : Singleton<DisplayViewManager>
	{
		[SerializeField]
		private GUISkin guiSkin;
		private DisplayParameters userParameters;
		private DisplayParameters screenParameters;
		private Rect viewArea;
		private int fps = 0;

		public GUISkin GUISkin
		{
			get { return this.guiSkin; }
		}

		protected override void Awake()
		{
			base.Awake();

			// ユーザーパラメータ・画面パラメータの初期化を行う
			this.userParameters = new DisplayParameters();
			this.screenParameters = new DisplayParameters();

			// 画面パラメータの情報を設定する
			this.screenParameters.SetParameter("Width", Screen.width);
			this.screenParameters.SetParameter("Height", Screen.height);
			this.screenParameters.SetParameter("fps", () => this.fps, this);

			// 表示エリアを設定する
			this.viewArea = new Rect(10, 10, Screen.width - 20, Screen.height - 20);
		}

		public static void SetParameter<TValue>(
			string key, Binder<TValue> binder, GameBehaviour unityObject)
		{
			DisplayViewManager manager = DisplayViewManager.Instance;
			if (manager != null)
			{
				manager.userParameters.SetParameter<TValue>(key, binder, unityObject);
			}
		}

		public static void SetParameter<TValue>(string key, TValue value)
		{
			DisplayViewManager manager = DisplayViewManager.Instance;
			if (manager != null)
			{
				manager.userParameters.SetParameter<TValue>(key, value);
			}
		}

		void Update()
		{
			this.fps = Mathf.RoundToInt(1 / Time.deltaTime);
		}

		void OnGUI()
		{
			GUI.skin = this.GUISkin;
			ViewUserParameters();
			ViewScreenParameters();
		}
		// ユーザーパラメータ情報を表示する（左上）
		private void ViewUserParameters()
		{
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			GUILayout.BeginArea(this.viewArea);
			GUILayout.BeginVertical();
			foreach (string key in this.userParameters.Keys)
			{
				GUILayout.Label(key + ": " + this.userParameters[key].Parameter);
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
		// 画面情報を表示する（右下）
		private void ViewScreenParameters()
		{
			GUI.skin.label.alignment = TextAnchor.UpperRight;
			GUILayout.BeginArea(this.viewArea);
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			foreach (string key in this.screenParameters.Keys)
			{
				GUILayout.Label(key + ": " + this.screenParameters[key].Parameter);
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}
}

