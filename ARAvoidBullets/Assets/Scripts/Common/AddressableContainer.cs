using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.Video;
using System.Linq;

namespace Almond
{
	[CreateAssetMenu(fileName = "AddressableContainer", menuName = "Almond")]
	public class AddressableContainer : ScriptableObject
	{
		private class Addressable
		{
			public string key;
			public AssetReference asset;
		}

		[SerializeField] private Addressable[] container;

		public async UniTask<GameObject> Instance(string key)
		{
			var assetRef = container.FirstOrDefault(x => x.key == key);
			if(key == null)
			{
				Debug.LogWarning($"AssetReference not found :: {key}");
				return null;
			}

			return await Addressables.InstantiateAsync(assetRef);
		}
		public async UniTask<T> InstanceComponent<T>(string key) where T : Component
		{
			var gameObject = await Instance(key);
			return gameObject.GetComponent<T>();
		}
	}
}