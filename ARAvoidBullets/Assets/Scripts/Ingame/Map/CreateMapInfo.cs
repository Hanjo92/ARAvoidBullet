using UnityEngine;

namespace ARAvoid
{
	public struct CreateMapInfo
	{
		public Vector3 rootPosition;
		public Vector3 rootEuler;
		public Vector3[] positions;

		public CreateMapInfo(Vector3 pos, Vector3 angle, Vector3[] vPos)
		{
			rootPosition = pos;
			rootEuler = angle;
			positions = vPos;
		}
	}
}