using System;
using UnityEngine;
using Almond;

namespace ARAvoid
{
	[RequireComponent(typeof(MeshRenderer))]
	public class BulletBase : MonoBehaviour, IPoolObj
	{
		[SerializeField] private string key;
		public string TemplateKey => key;

		private Vector3 direction;

		private void Awake()
		{
			
		}

		public void Init(object[] param = null)
		{
			
		}

		public void Release()
		{
			SimplePool.Release( this );
		}

		public void BulletMove(float deltaTime)
		{

		}
	}
}