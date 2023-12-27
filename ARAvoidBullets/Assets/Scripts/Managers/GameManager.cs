using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Almond;
using System.Threading;
using System.Linq;

namespace ARAvoid
{
	public enum GameState
	{
		None,
		Main,
		Option,
		Map,
		Play,
	}

	public class GameManager : MonoSingleton<GameManager>
	{
		#region ScriptableObject
		[SerializeField] private MaterialContainer materialContainer;
		[SerializeField] private AddressableContainer addressableContainer;
		#endregion

		[SerializeField] private EffectManager effectManager;
		private Dictionary<string, GameStateBase> gameStates = new Dictionary<string, GameStateBase>();

		public MaterialContainer MaterialContainer => materialContainer;
		public static AddressableContainer AddressableContainer => Instance.addressableContainer;
		public EffectManager EffectManager => effectManager;

		private CancellationTokenSource cts;

		private SaveData saveData;
		public float HighScore => saveData.maxTime;

		private GameStateBase currentState;
		private GameState state;
		public GameState GameState => state;

		private CreateMapInfo mapInfo;
		public void SaveMapPositions(CreateMapInfo positions) => mapInfo = positions;
		public CreateMapInfo GetMapInfo() => mapInfo;
		private Map map;
		public async UniTask SetMap()
		{
			if(map == null)
			{
				map = await addressableContainer.InstanceComponent<Map>("Map");
			}
			map.gameObject.SetActive(true);
			map.Setup(GetMapInfo());
		}
		public void DisableMap()
		{
			if(map != null)
				map.gameObject.SetActive(false);
		}

		protected override void Awake()
		{
			base.Awake();
			Initialize().Forget();			
		}
		private async UniTaskVoid Initialize()
		{
			SimplePool.AddressableContainer = addressableContainer;
			saveData = SaveData.Load();

			for(var s = GameState.Main; s <= GameState.Play; s++)
			{
				var state = await addressableContainer.InstanceComponent<GameStateBase>($"{s}State");
				if(state == null)
				{
					Debug.LogError($"{s} state not found");
					continue;
				}
				gameStates.Add(s.ToString(), state);
			}
			await ThemaManager.Inst.Initialize();
			ThemaManager.Inst.AddListener((t, b) =>
			{
				if(materialContainer == null)
					return;
				
				SetUIMaterials();
			});
			SetUIMaterials();
			cts = new CancellationTokenSource();
			await ChangeState(GameState.Main);

			void SetUIMaterials()
			{
				var colors = ThemaManager.Inst.GetThemaColors();
				materialContainer.ApplyThemaToUIMaterials(colors);
				materialContainer.ApplyThemaToTMP(colors);
			}
		}
		public async UniTask ChangeState(GameState gameState)
		{
			cts.Cancel();
			await UniTask.WaitUntil(() => cts.IsCancellationRequested);

			if(currentState != null)
			{
				await currentState.OnLeaveState();
			}

			if(gameStates.TryGetValue(gameState.ToString(), out var next))
			{
				state = gameState;
				currentState = next;
				await currentState.OnEnterState();
				cts = new CancellationTokenSource();
				currentState.ProcessState(cts).Forget();
			}
		}
	}
}