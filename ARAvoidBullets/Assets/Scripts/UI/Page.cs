using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace ARAvoid
{
	public interface Page
	{
		string Key { get; }
		UniTask Active();
		UniTask Inactive();
	}
}
