using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine.Video;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace ARAvoid
{
	/// <summary>
	// Ridar Setup
	// await UniTask.WaitUntil(() => setUp );
	/// </summary>

	public class MapGenerator : MonoBehaviour
	{
		[SerializeField] private VertexPoint[] vertexPositions;
		[SerializeField] AssetReference pointPrefab;

		private Transform mapRoot;
		private bool setup = false;

		private Camera view;

		private void Awake()
		{
			mapRoot = new GameObject("MapRoot").transform;
			view = Camera.main;
		}

		public void SetUpEnd() => setup = true;

		public async UniTask SetupPositions()
		{
			setup = false;
			mapRoot.position = view.transform.position + view.transform.forward * Defines.MapMaxAwayDistance;

			if(vertexPositions != null && mapRoot != null)
			{
				mapRoot.gameObject.SetActive(true);
				
				if(vertexPositions.Length == 0)
				{
					await CreatePoints();
					return;
				}

				foreach(var pos in vertexPositions)
				{
					var coord = pos.transform.localPosition;
					coord.y = 0;
					SetPosition(pos.transform, coord);
					await UniTask.Delay(TimeSpan.FromSeconds(Defines.DefaultScaleTime * 0.5f));
				}
				return;
			}

			async UniTask CreatePoints()
			{
				var verticsCount = Defines.MapVerticsCount;
				var size = Defines.MapSize;

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
						position.x += Defines.MapVerticsInterval;

					}
					position.z += Defines.MapVerticsInterval;
					await UniTask.Delay(TimeSpan.FromSeconds(Defines.DefaultScaleTime * 0.5f));
				}
				for(int v = 0; v < verticsCount; v++)
				{
					for(int h = 0; h < verticsCount; h++)
					{
						var index = h + v * verticsCount;
						if(h < verticsCount - 1)
						{
							vertexPositions[index].SetNeighbor(vertexPositions[index + 1], Direction.Right, true);
						}
						if(v < verticsCount - 1)
						{
							vertexPositions[index].SetNeighbor(vertexPositions[index + verticsCount], Direction.Forward, true);
						}
					}
				}
			}
		}

		public void DisableGenerate()
		{
			foreach(var pos in vertexPositions)
			{
				var coord = pos.transform.localPosition;
				coord.y = 0;
				SetPosition(pos.transform, coord, false);
				//await UniTask.Delay(TimeSpan.FromSeconds(Defines.DefaultScaleTime * 0.5f));
			}
			mapRoot.gameObject.SetActive(false);
		}

		private void SetPosition(Transform position, Vector3 localPosition, bool apear = true)
		{
			position.transform.localPosition = localPosition;
			position.transform.localScale = apear ? Vector3.zero : (Vector3.one * 0.05f);
			position.transform.DOScale(apear ? (Vector3.one * 0.05f) : Vector3.zero, Defines.DefaultScaleTime).SetEase(Ease.OutCirc);
		}

		public async UniTask<CreateMapInfo> MapSetting()
		{
			await UniTask.WaitUntil(() =>
			{
				var position = view.transform.position;
				var viewDirection = view.transform.forward;
				var distance = Defines.MapMaxAwayDistance;
				if(Physics.Raycast(position, viewDirection, out var info, Defines.MapMaxAwayDistance, Defines.MapMask))
				{
					distance = info.distance;
				}
				mapRoot.position = position + viewDirection * distance;

				var euler = mapRoot.eulerAngles;
				euler.y = view.transform.eulerAngles.y;
				mapRoot.eulerAngles = euler;

				foreach(var pos in vertexPositions)
					pos.UpdateHeight();
				foreach(var pos in vertexPositions)
					pos.UpdateLine();

				return setup;
			});
			
			var positions = new List<Vector3>();
			foreach(var obj in vertexPositions)
			{
				positions.Add(obj.transform.localPosition);
			}

			return new CreateMapInfo(mapRoot.position, mapRoot.eulerAngles, positions.ToArray());
		}
	}
}