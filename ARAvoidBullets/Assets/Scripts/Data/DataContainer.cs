using UnityEngine;
using System;
using Almond;
using System.Linq;

namespace ARAvoid
{
	[CreateAssetMenu( fileName = "DataContainer", menuName = "ARAvoid" )]
	public class DataContainer : ScriptableObject
	{
		[SerializeField] private Material[] MapThemaMaterials = new Material[typeof(Thema).Lenght()];
		public Material GetMapMaterial( Thema thema ) => MapThemaMaterials[ (int)thema ];

		[ Serializable]
		public class BulletMaterials
		{
			public string BulletKey;
			public Material[] materials = new Material[ typeof( Thema ).Lenght() ];
			public Material GetMapMaterial( Thema thema ) => materials[ (int)thema ];
		}
		[SerializeField] private BulletMaterials[] bulletMaterials;
		public Material GetMapMaterial(string key , Thema thema ) 
			=> bulletMaterials.FirstOrDefault((b)=>b.BulletKey == key)?.materials[ (int)thema ] ?? null;
	}
}