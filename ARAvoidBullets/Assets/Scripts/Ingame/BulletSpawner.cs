using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace ARAvoid
{
	public class BulletSpawner
	{
		private HashSet<BulletBase> bullets = new HashSet<BulletBase>();
		private float coolTime = 0.5f;
		private float fTime;
		private int bulletCount;

		public void Update(float deltaTime)
		{
			// spawn
			fTime += deltaTime;
			if(fTime > coolTime)
			{

				fTime -= coolTime;
			}

			foreach(var bullet in bullets)
			{
				if(bullet.IsActive == false)
					continue;

				bullet.BulletUpdate(deltaTime);
			}
		}
		public void BulletMove(float deltaTime)
		{
			foreach(var bullet in bullets)
			{
				if(bullet.IsActive == false)
					continue;

				bullet.BulletMove(deltaTime);
			}
		}

		public async UniTask DisableAllBullets()
		{
			foreach(var bullet in bullets)
			{
				if(bullet.IsActive == false)
					continue;

				bullet.Release();
				await UniTask.Yield();
			}
		}
	}
}