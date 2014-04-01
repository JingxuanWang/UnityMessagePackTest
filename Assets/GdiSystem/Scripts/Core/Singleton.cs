
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GdiSystem
{
	public class Singleton<T> : GameBehaviour where T : Singleton<T>
	{
		private static Type instanceType;
		private static T instance;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType(instanceType) as T;
					if (instance == null)
					{
					}
				}
				return instance;
			}
		}

		protected virtual void Awake()
		{
			Application.targetFrameRate = 60;

			// 型をチェックする
			if (!(this is T))
			{
				throw new Exception(this.GetType().ToString() + "はSingleton<T>を不正な形で継承しています");
			}

			// 親のGameObjectが存在する場合はエラーとする
			if (transform.parent != null)
			{
				throw new Exception(
					this.GetType().ToString() + "は" + transform.parent.gameObject.name +
					"の子オブジェクトにアタッチされています。トップのGameObjectにアタッチして下さい");
			}

			// 空のGameObjectに自分自信のみアタッチされているかチェックする。
			if (GetComponentsInChildren<Component>().Count() != 2)
			{
				throw new Exception(
					gameObject.name + "に" + this.GetType().ToString() +
					"以外のコンポーネントがアタッチされています");
			}

			// 実行時のタイプを取得する(For FindObjectOfType)
			instanceType = this.GetType();

			// 最初のインスタンスでなければアタッチ元のGameObjectを削除する
			if (this.GetInstanceID() != Instance.GetInstanceID())
			{
				Destroy(gameObject);
				return;
			}

			// 新しいシーンがロードされても破棄されないようにする
			DontDestroyOnLoad(this);
		}
	}
}

