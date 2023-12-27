
using UnityEngine;

namespace ARAvoid
{
	public static class GamePhysics
	{
		public static float GetMapY(Vector3 targetPos)
		{
			var height = targetPos.y;
			height += Defines.MaxMapHeight;

			if(Physics.Raycast(targetPos, Vector3.down, out var info, Defines.MaxMapHeight * 2f, Defines.MapMask))
			{
				height = info.point.y;
			}
			else
			{
				height = targetPos.y;
			}

			return height;
		}
	}
}