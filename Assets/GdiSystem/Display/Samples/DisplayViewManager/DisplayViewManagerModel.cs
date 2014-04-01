using UnityEngine;
using System.Collections;
using GdiSystem.Display;

// DisplayViewManager Sample
// DisplayViewManagerの複数の使い方について示したサンプル
namespace GdiSystem.Samples
{
	public class DisplayViewManagerModel : GameBehaviour
	{
		private int x;

		void Start()
		{
			// この方法でパラメータを表示したい場合は、第3引数にGameBehaviourを継承したインスタンスを指定する必要がある
			DisplayViewManager.SetParameter<int>("x", () => this.x, this);

		}

		void Update()
		{
			DisplayViewManager.SetParameter<float>("Time", Time.time);
			this.x++;
		}
	}
}