using Almond;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ARAvoid
{
	public class Player : Singleton<Player>
	{
		private PlayerActor actor;

		// Stat
		private float speed = 3;
		public float Speed => speed;
		private Map currentMap;

		private float fTime = 0;
		public float CoolTimeRatio => Mathf.Clamp(fTime / Defines.DashCoolTime, 0, 1);
		public bool CanDash => CoolTimeRatio >= 1f;
		private bool isDead = false;
		public bool IsDead => isDead;
		public void Initialize(Map map)
		{
			currentMap = map;
			speed = Defines.PlayerDefaultSpeed;
			isDead = false;
		}

		public void Move(Vector3 direction, float deltaTime)
		{
			var current = actor.Position;
			var next = current + (direction * Defines.MapVerticsInterval);

			actor.ActorMove(next);
		}
		public void Dash()
		{
			if(CanDash == false)
				return;
			fTime = 0;
		}

		public void Contect()
		{
			isDead = true;
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
