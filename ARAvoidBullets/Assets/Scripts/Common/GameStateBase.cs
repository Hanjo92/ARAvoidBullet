using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Almond
{
	public abstract class GameStateBase : MonoBehaviour
	{
		public abstract string Key { get; }
		public abstract UniTask OnEnterState();
		public abstract UniTask OnLeaveState();
		public abstract UniTask ProcessState(CancellationTokenSource cts);
	}
}