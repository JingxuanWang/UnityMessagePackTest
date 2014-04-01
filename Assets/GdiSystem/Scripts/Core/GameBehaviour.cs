using System;
using System.Collections.Generic;
using UnityEngine;

namespace GdiSystem
{
	//	public interface IUnityObject
	//	{
	//		event DestroyHandler DestroyEvent;
	//
	//		int GetInstanceID();
	//	}
	public class UnityObjectComparer<T> : EqualityComparer<T>
		where T : UnityEngine.Object
	{
		public override int GetHashCode(T obj)
		{
			return obj.GetInstanceID();
		}

		public override bool Equals(T x, T y)
		{
			return this.GetHashCode(x) == this.GetHashCode(y);
		}
	}
	/// <summary>
	/// GameGameBehaviourが破棄される直前にコールされる。引数はGameBehaviourのInstanceID
	/// </summary>
	public delegate void DestroyHandler(GameBehaviour unityObject);
	public class GameBehaviour : MonoBehaviour
	{
		public event DestroyHandler DestroyEvent;

		/// <summary>
		/// GameBehaviourが破棄される直前にコールされる
		/// </summary>
		protected virtual void OnDestroy()
		{
			if (DestroyEvent != null)
			{
				DestroyEvent(this);
			}
			DestroyEvent = null;
		}
	}

	public class NotifyEventArgs<TNotice> : EventArgs
	{
		private TNotice notice;

		public TNotice Notice
		{
			get { return this.notice; }
		}

		public NotifyEventArgs(TNotice notice) : base()
		{
			this.notice = notice;
		}
	}
	public delegate void NotifyEventHandler<TNotice>(object sender,NotifyEventArgs<TNotice> args);
}
