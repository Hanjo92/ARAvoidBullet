using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Almond;

namespace ARAvoid
{
	[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
	public class Map : MonoBehaviour
	{
		private MeshFilter meshFilter;
		private MeshRenderer meshRenderer;
		private Mesh mesh;

		private Vector3[] vertices;
		private void Awake()
		{
			meshFilter = GetComponent<MeshFilter>();
			meshRenderer = GetComponent<MeshRenderer>();
			mesh = meshFilter.mesh;
		}

		public void SetupVertices(Vector3[] coords)
		{
			vertices = coords;
			mesh.SetVertices( vertices );
			mesh.RecalculateNormals();

		}

		public void SetThema(Thema thema)
		{
			var mapMaterial = meshRenderer.sharedMaterial;
			var themaColor = ThemaManager.Inst.GetThemaColors();

			mapMaterial.SetColor("_MainColor", themaColor.main1);
			mapMaterial.SetColor("_TrailColor", themaColor.dark);
		}
	}
}