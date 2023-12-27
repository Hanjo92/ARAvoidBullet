using Almond;
using UnityEngine;

namespace ARAvoid
{
	public abstract class ActorBase : MonoBehaviour, IPoolObj
	{
		public abstract string TemplateKey { get; }

		public abstract void Init(object[] param = null);

		public abstract void Release();

		public virtual void ActorMove(Vector3 newPos) { }
	}
}