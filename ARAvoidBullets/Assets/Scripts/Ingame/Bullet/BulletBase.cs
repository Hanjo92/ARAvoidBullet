using System;
using UnityEngine;
using Almond;
using UnityEditor.Rendering;
using UnityEngine.Video;

namespace ARAvoid
{
	[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
	public class BulletBase : MonoBehaviour, IPoolObj
	{
		[SerializeField] private string key;
		public string TemplateKey => key;
		private Vector3 direction;

		private Rigidbody rb;

		protected virtual float Speed => Defines.BulletSpeed;
		public bool IsActive => gameObject.activeSelf;

		private void Awake()
		{
			rb = GetComponent<Rigidbody>();
		}

		public void Init(object[] param = null)
		{
			
		}

		public void Release()
		{
			SimplePool.Release(this);
		}

		public virtual void BulletUpdate(float deltaTime)
		{
			
		}
		public virtual void BulletMove(float deltaTime)
		{
			var distance = Speed * deltaTime;
			var nextPosition = (direction * distance) + transform.position;
			nextPosition.y = GamePhysics.GetMapY(nextPosition);

			rb.MovePosition(nextPosition);
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				Player.Inst.Contect();
			}
		}
	}
}