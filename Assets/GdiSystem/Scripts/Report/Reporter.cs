using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GdiSystem
{
	public delegate void ReportHandler<TResult,TResultArgs>(
		string listenerKey,TResult result,TResultArgs args);
	/// <summary>
	/// レポートをリッスンするためのオブジェクト
	/// </summary>
	public class ReportListener<TResult, TResultArgs>
	{
		public ReportListener(ReportHandler<TResult,TResultArgs> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.Handler = handler;
		}

		public ReportHandler<TResult,TResultArgs> Handler { get; private set; }
	}

	public class ReportResultSender<TListener, TResult, TResultArgs>
		where TListener : ReportListener<TResult, TResultArgs>
	{
		private string listenerKey;
		private TListener listener;
		private TResult result;
		private TResultArgs args;

		public ReportResultSender(
			string listenerKey, TListener listener, TResult result, TResultArgs args)
		{
			this.listenerKey = listenerKey;
			this.listener = listener;
			this.result = result;
			this.args = args;
		}

		public void Send()
		{
			this.listener.Handler(this.listenerKey, this.result, this.args);
		}
	}

	public class ReportBinder<TListener, TResult, TResultArgs>
		where TListener : ReportListener<TResult, TResultArgs>
	{
		private Dictionary<string, TListener> entry = new Dictionary<string, TListener>();

		public Dictionary<string, TListener> Entry
		{
			get { return this.entry; }
		}

		public void Add(string listenerKey, TListener listener)
		{
			this.entry[listenerKey] = listener;
		}

		public void Remove(string listenerKey)
		{
			this.entry.Remove(listenerKey);
		}
	}

	public class Reporter<TListener, TResult, TResultArgs, TBinder> : GameBehaviour
		where TBinder : ReportBinder<TListener, TResult, TResultArgs>, new()
		where TListener : ReportListener<TResult, TResultArgs>
	{
		private TBinder binder = new TBinder();

		public Dictionary<string, TListener> Entry
		{
			get{ return binder.Entry; }
		}

		protected void Add(string listenerKey, TListener listener)
		{
			binder.Add(listenerKey, listener);
		}

		protected void Remove(string listenerKey)
		{
			binder.Remove(listenerKey);
		}
	}

	public class SingletonReporter<TListener, TResult, TResultArgs, TBinder> :
		Singleton<SingletonReporter<TListener, TResult, TResultArgs, TBinder>>
		where TBinder : ReportBinder<TListener, TResult, TResultArgs>, new()
		where TListener : ReportListener<TResult, TResultArgs>
	{
		private TBinder binder = new TBinder();

		public Dictionary<string, TListener> Entry
		{
			get{ return binder.Entry; }
		}

		protected void Add(string listenerKey, TListener listener)
		{
			binder.Add(listenerKey, listener);
		}

		protected void Remove(string listenerKey)
		{
			binder.Remove(listenerKey);
		}
	}
}