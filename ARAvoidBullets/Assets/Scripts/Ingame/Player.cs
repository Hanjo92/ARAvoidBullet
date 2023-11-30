using Almond;
using UnityEngine;

namespace ARAvoid
{
	public class Player : Singleton<Player>
	{
		private Transform actor;

		// Stat
		private float speed = 3;
		public float Speed => speed;
		private Map currentMap;

		private float fTime = 0;
		public float CoolTimeRatio => Mathf.Clamp(fTime / Defines.DashCoolTime, 0, 1);
		public bool CanDash => CoolTimeRatio >= 1f;

		public void Initialize(Map map)
		{
			currentMap = map;
			speed = Defines.PlayerDefaultSpeed;
		}

		public void Move(float horizontal, float vertical, float deltaTime)
		{
			var direction = Vector3.forward * vertical + Vector3.right * horizontal;
			var current = actor.position;
			var next = current + (direction * Defines.MapVerticsInterval);
			
		}
		public void Dash()
		{
			if(CanDash == false)
				return;
			fTime = 0;
		}

		public void Contect()
		{

		}

		public void PlayerUpdate(Vector3 direction, float deltaTime)
		{
			if(CanDash == false)
			{
				fTime += deltaTime;
			}

		}
	}
}
