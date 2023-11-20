using ARAvoid;
using UnityEditor;
using UnityEngine;


namespace ARAvoid
{
	public class VertexPoint : MonoBehaviour
	{
		private Transform root;
		private Transform Root => root ??= transform.parent;

		[SerializeField] private VertexPoint[] neighbors = new VertexPoint[4];
		[SerializeField] private LayerMask fieldLayer;
		[SerializeField] private LineRenderer lineRenderer;
		public Vector3 Position => transform.position;
		public float Heights => transform.position.y;
		private int lineCount = 0;
		private bool lineCountCheck = false;

		public void UpdateHeight()
		{
			var layPoint = transform.position;
			layPoint.y = (Root ? root.position.y : transform.position.y) + Defines.MaxMapHeight;

			if(Physics.Raycast(layPoint, Vector3.down, out var hitInfo, Defines.MaxMapHeight * 2f, fieldLayer))
			{
				layPoint.y = hitInfo.point.y;
				transform.position = layPoint;
			}
		}

		public void SetNeighbor(VertexPoint vertex, Direction direction, bool neighbor)
		{
			neighbors[(int)direction] = vertex;

			if(neighbor && vertex != null)
			{
				vertex.SetNeighbor(this, direction.Opposite(), false);
			}
		}

		public void UpdateLine()
		{
			if(lineRenderer == null || lineRenderer.enabled == false)
				return;
			
			if(lineCountCheck == false)
			{
				if(neighbors[(int)Direction.Right])
					lineCount++;
				if(neighbors[(int)Direction.Forward])
					lineCount++;
				if(lineCount > 0)
					lineCount++;

				lineRenderer.positionCount = lineCount;
				var lineWidth = transform.localScale.x * 0.3f;
				lineRenderer.startWidth = lineWidth;
				lineRenderer.endWidth = lineWidth;

				lineCountCheck = true;
			}
			if(lineCount == 0)
			{
				lineRenderer.enabled = false;
				return;
			}

			if(lineCount == 3)
			{
				lineRenderer.SetPosition(0, neighbors[(int)Direction.Right].Position);
				lineRenderer.SetPosition(1, transform.position);
				lineRenderer.SetPosition(2, neighbors[(int)Direction.Forward].Position);
			}
			else if(lineCount == 2)
			{
				if(neighbors[(int)Direction.Right] != null)
				{
					lineRenderer.SetPosition(0, neighbors[(int)Direction.Right].Position);
					lineRenderer.SetPosition(1, transform.position);
				}
				else
				{
					lineRenderer.SetPosition(0, neighbors[(int)Direction.Forward].Position);
					lineRenderer.SetPosition(1, transform.position);
				}
			}
		}
	}
}