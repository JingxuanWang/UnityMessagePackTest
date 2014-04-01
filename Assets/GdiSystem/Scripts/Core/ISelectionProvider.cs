using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GdiSystem
{
	public interface ISelectionProvider<TItem>
	{
		HashSet<TItem> Items { get; }
	}
}