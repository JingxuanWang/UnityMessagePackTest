using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace GdiSystem.Display.Internal
{
	internal delegate bool DictionaryMatchDelegate<TKey,TValue>(TKey tkey,TValue tvalue);
	internal static class DictionaryExtention
	{
		internal static void RemoveAll<TKey, TValue>(
			this IDictionary<TKey, TValue> dictionary, DictionaryMatchDelegate<TKey, TValue> match)
		{
			foreach (TKey key in new List<TKey>(dictionary.Keys).FindAll(k => match(k, dictionary[k])))
			{
				dictionary.Remove(key);
			}
		}
	}

	internal interface IDisplayParameter
	{
		string Parameter { get; }
	}

	internal interface IUnityObjectParameter : IDisplayParameter
	{
		int SourceInstanceID { get; }
	}

	internal struct PrimitiveParameter<TValue> : IDisplayParameter
	{
		public PrimitiveParameter(TValue parameter) : this()
		{
			this.Parameter = parameter.ToString();
		}

		public string Parameter
		{
			get;
			private set;
		}
	}

	internal class UnityObjectParameter<TValue> : IUnityObjectParameter
	{
		public UnityObjectParameter(Binder<TValue> binder, int sourceInstanceID)
		{
			this.SourceInstanceID = sourceInstanceID;
			this.Binder = binder;
		}

		public int SourceInstanceID { get; private set; }

		public Binder<TValue> Binder { get; private set; }

		public string Parameter
		{
			get
			{
				TValue v = Binder();
				return v != null ? v.ToString() : "";
			}
		}
	}

	internal class DisplayParameters
	{
		private Dictionary<string, IDisplayParameter> entry = new Dictionary<string, IDisplayParameter>();

		public void SetParameter<TValue>(
			string key, Binder<TValue> binder, GameBehaviour unityObject)
		{
			if (!entry.ContainsKey(key))
			{
				unityObject.DestroyEvent += this.OnDestroyUnityObject;
			}
			this[key] = new UnityObjectParameter<TValue>(binder, unityObject.GetInstanceID());

		}

		public void SetParameter<TValue>(string key, TValue value)
		{
			this[key] = new PrimitiveParameter<TValue>(value);
		}

		public void Remove(string key)
		{
			this.entry.Remove(key);
		}

		#region ジェネリックのイベントデリゲートは使えなかったためiOS用にインターフェースを用意

		private void OnDestroyUnityObject(GameBehaviour unityObject)
		{
			this.entry.RemoveAll(
				(key, value) => value is IUnityObjectParameter &&
				((IUnityObjectParameter)value).SourceInstanceID ==
				unityObject.GetInstanceID());
		}

		#endregion

		public IDisplayParameter this [string key]
		{
			get
			{
				IDisplayParameter parameter;
				this.entry.TryGetValue(key, out parameter);
				return parameter;
			}
			private set
			{
				this.entry[key] = value;
			}
		}

		public Dictionary<string, IDisplayParameter>.KeyCollection Keys
		{
			get{ return this.entry.Keys; }
		}
	}
}