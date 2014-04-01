using UnityEngine;
using System.Collections;
using GdiSystem;

namespace GdiSystem
{
	// EasingのType
	public enum EasingType
	{
		Linear,
		Clerp,
		Spring,
		EaseInQuad,
		EaseOutQuad,
		EaseInOutQuad,
		EaseInCubic,
		EaseOutCubic,
		EaseInOutCubic,
		EaseInQuart,
		EaseOutQuart,
		EaseInOutQuart,
		EaseInQuint,
		EaseOutQuint,
		EaseInOutQuint,
		EaseInSine,
		EaseOutSine,
		EaseInOutSine,
		EaseInExpo,
		EaseOutExpo,
		EaseInOutExpo,
		EaseInCirc,
		EaseOutCirc,
		EaseInOutCirc,
		EaseInBounce,
		EaseOutBounce,
		EaseInOutBounce,
		EaseInBack,
		EaseOutBack,
		EaseInOutBack,
		//Punch,
		EaseInElastic,
		EaseOutElastic,
		EaseInOutElastic,
	}
	public delegate float EasingDelegate(float start,float end,float value);
	public static class EasingTypeExtension
	{
		public static EasingDelegate GetFunction(this EasingType easingType)
		{
			switch (easingType)
			{
				case EasingType.Linear:
					return new EasingDelegate(Easing.Linear);
				case EasingType.Clerp:
					return new EasingDelegate(Easing.Clerp);
				case EasingType.Spring:
					return new EasingDelegate(Easing.Spring);
				case EasingType.EaseInQuad:
					return new EasingDelegate(Easing.EaseInQuad);
				case EasingType.EaseOutQuad:
					return new EasingDelegate(Easing.EaseOutQuad);
				case EasingType.EaseInOutQuad:
					return new EasingDelegate(Easing.EaseInOutQuad);
				case EasingType.EaseInCubic:
					return new EasingDelegate(Easing.EaseInCubic);
				case EasingType.EaseOutCubic:
					return new EasingDelegate(Easing.EaseOutCubic);
				case EasingType.EaseInOutCubic:
					return new EasingDelegate(Easing.EaseInOutCubic);
				case EasingType.EaseInQuart:
					return new EasingDelegate(Easing.EaseInQuart);
				case EasingType.EaseOutQuart:
					return new EasingDelegate(Easing.EaseOutQuart);
				case EasingType.EaseInOutQuart:
					return new EasingDelegate(Easing.EaseInOutQuart);
				case EasingType.EaseInQuint:
					return new EasingDelegate(Easing.EaseInQuint);
				case EasingType.EaseOutQuint:
					return new EasingDelegate(Easing.EaseOutQuint);
				case EasingType.EaseInOutQuint:
					return new EasingDelegate(Easing.EaseInOutQuint);
				case EasingType.EaseInSine:
					return new EasingDelegate(Easing.EaseInSine);
				case EasingType.EaseOutSine:
					return new EasingDelegate(Easing.EaseOutSine);
				case EasingType.EaseInOutSine:
					return new EasingDelegate(Easing.EaseInOutSine);
				case EasingType.EaseInExpo:
					return new EasingDelegate(Easing.EaseInExpo);
				case EasingType.EaseOutExpo:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseInOutExpo:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseInCirc:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseOutCirc:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseInOutCirc:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseInBounce:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseOutBounce:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseInOutBounce:
					return new EasingDelegate(Easing.EaseOutExpo);
				case EasingType.EaseInBack:
					return new EasingDelegate(Easing.EaseInBack);
				case EasingType.EaseOutBack:
					return new EasingDelegate(Easing.EaseOutBack);
				case EasingType.EaseInOutBack:
					return new EasingDelegate(Easing.EaseInOutBack);
			//case EasingType.Punch:
			//return new EasingDelegate(Easing.Punch);
				case EasingType.EaseInElastic:
					return new EasingDelegate(Easing.EaseInElastic);
				case EasingType.EaseOutElastic:
					return new EasingDelegate(Easing.EaseOutElastic);
				case EasingType.EaseInOutElastic:
					return new EasingDelegate(Easing.EaseInOutElastic);
				default:
					return new EasingDelegate(Easing.Linear);
			}
		}
	}
}
