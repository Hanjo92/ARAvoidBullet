using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine.Video;

namespace ARAvoid
{
	/// <summary>
	// Ridar Setup
	// await UniTask.WaitUntil(() => setUp );
	/// </summary>

	public class MapGenerator : MonoBehaviour
	{
		public int verticsCount = 10;
		public float size = 10f;

		[SerializeField] private VertexPoint[] vertexPositions;
		[SerializeField] AssetReference pointPrefab;
		private float Interval => size / verticsCount;

		private Transform mapRoot;
		private bool setup = false;

		private Camera view;

		private void Awake()
		{
			mapRoot = new GameObject("MapRoot").transform;
			view = Camera.main;
		}

		public async UniTask SetupPositions()
		{
			setup = false;

			if(vertexPositions != null && mapRoot != null)
			{
				mapRoot.gameObject.SetActive(true);
				foreach(var pos in vertexPositions)
				{
					var coord = pos.transform.localPosition;
					coord.y = 0;
					SetPosition(pos.transform, coord);
					await UniTask.Delay(TimeSpan.FromSeconds(Defines.DefaultScaleTime * 0.5f));
				}

				return;
			}

			vertexPositions = new VertexPoint[verticsCount * verticsCount];
			Vector3 position = Vector3.zero;
			position.z = size * -0.5f;

			for(int v = 0; v < verticsCount; v++)
			{
				position.x = size * -0.5f;
				for(int h = 0; h < verticsCount; h++)
				{
					var index = h + v * verticsCount;
					var obj = await pointPrefab.InstantiateAsync();
					vertexPositions[index] = obj.GetComponent<VertexPoint>();
					vertexPositions[index].transform.SetParent(mapRoot);
					SetPosition(vertexPositions[index].transform, position);
					position.x += Interval;
					await UniTask.Delay(TimeSpan.FromSeconds(Defines.DefaultScaleTime * 0.5f));
				}
				position.z += Interval;
			}
		}

		public async UniTask DisableGenerate()
		{
			foreach(var pos in vertexPositions)
			{
				var coord = pos.transform.localPosition;
				coord.y = 0;
				SetPosition(pos.transform, coord, false);
				await UniTask.Delay(TimeSpan.FromSeconds(Defines.DefaultScaleTime * 0.5f));
			}
			mapRoot.gameObject.SetActive(false);
		}

		private void SetPosition(Transform position, Vector3 localPosition, bool apear = true)
		{
			position.transform.localPosition = localPosition;
			position.transform.localScale = apear ? Vector3.zero : Vector3.one;
			position.transform.DOScale(apear ? Vector3.one : Vector3.zero, Defines.DefaultScaleTime).SetEase(Ease.OutCirc);
		}

		public async UniTask<Vector3[]> MapSetting()
		{
			await UniTask.WaitUntil(() =>
			{
				var position = view.transform.position;
				var viewDirection = view.transform.forward;
				var distance = Defines.MapMaxAwayDistance;
				if(Physics.Raycast(position, viewDirection, out var info, Defines.MapMaxAwayDistance, Defines.FieldLayer))
				{
					distance = info.distance;
				}
				mapRoot.position = position + viewDirection * distance;

				foreach(var pos in vertexPositions)
					pos.UpdateHeight();

				return setup;
			});

			var positions = new List<Vector3>();
			foreach(var obj in vertexPositions)
			{
				positions.Add(obj.transform.position);
			}

			return positions.ToArray();
		}
	}
}