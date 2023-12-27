using Almond;
using UnityEngine;

namespace ARAvoid
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerActor : ActorBase
	{
		protected Rigidbody rb;

		public override string TemplateKey => throw new System.NotImplementedException();
		public Vector3 Position => transform.position;

		public override void Init(object[] param = null)
		{
			//throw new System.NotImplementedException();
		}

		public override void Release()
		{
			SimplePool.Release(this);
		}

		protected virtual void Awake()
		{
			rb = GetComponent<Rigidbody>();
		}

		public override void ActorMove(Vector3 newPos)
		{
			rb.MovePosition(newPos);
		}
	}
}