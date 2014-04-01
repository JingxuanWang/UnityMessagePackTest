using UnityEngine;
using System;
using System.Collections;

namespace GdiSystem
{
	public class CustomDescription : Attribute
	{
		public string Description { get; private set; }

		public CustomDescription(string description)
		{
			this.Description = description;
		}
	}
}