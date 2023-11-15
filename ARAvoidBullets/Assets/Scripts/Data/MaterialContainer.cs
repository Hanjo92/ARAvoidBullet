using UnityEngine;
using System;
using Almond;
using System.Linq;
using UnityEngine.Video;

namespace ARAvoid
{
	[CreateAssetMenu( fileName = "MaterialContainer", menuName = "ARAvoid/MaterialContainer")]
	public class MaterialContainer : ScriptableObject
	{
		[Header("Game")]
		[SerializeField] private Material[] mapThemaMaterials = new Material[typeof(Thema).Lenght()];
		public Material GetMapThemaMaterial( Thema thema ) => mapThemaMaterials[ (int)thema ];
		[Serializable]
		public class BulletMaterials
		{
			public string BulletKey;
			public Material[] materials = new Material[ typeof( Thema ).Lenght() ];
			public Material GetMapMaterial( Thema thema ) => materials[ (int)thema ];
		}
		[SerializeField] private BulletMaterials[] bulletMaterials;
		public Material GetBulletThemaMaterial(string key , Thema thema ) 
			=> bulletMaterials.FirstOrDefault((b)=>b.BulletKey == key)?.materials[ (int)thema ] ?? null;
		
		[Serializable]		
		public class UIMaterial
		{
			public ThemaColorType themaColor;
			public Material material;
		}

		[Header("UI")]
		[SerializeField] private UIMaterial[] uiMaterials;
		public void ApplyThemaToUIMaterials(ThemaColors themaColors)
		{
			foreach(var m in uiMaterials)
			{
				if(m.material)
				{
					m.material.SetColor("_MainColor", themaColors.GetColor(m.themaColor));
				}
			}
		}

		[Serializable]
		public class TMPMaterial
		{
			public ThemaColorType faceColor;
			public ThemaColorType outlineColor;
			public ThemaColorType underlayColor;
			public Material material;
		}
		[SerializeField] private TMPMaterial[] TMPMaterials;
		public void ApplyThemaToTMP(ThemaColors themaColors)
		{
			foreach(var m in TMPMaterials)
			{
				if(m.material)
				{
					if(m.material)
					{
						m.material.SetColor("_FaceColor", themaColors.GetColor(m.faceColor));
					}
					if(m.material)
					{
						m.material.SetColor("_OutlineColor", themaColors.GetColor(m.outlineColor));
					}
					if(m.material)
					{
						m.material.SetColor("_UnderlayColor", themaColors.GetColor(m.underlayColor));
					}
				}
			}
		}
	}
}