using Almond;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ARAvoid
{
	public class PausedPopup : PopupBase
	{
		[Space]
		[SerializeField] private Toggle leftHandToggle;
		[SerializeField] private Slider volumeSlider;
		[SerializeField] private Button homeButton;
		[SerializeField] private Button resumeButton;

		protected override void Awake()
		{
			base.Awake();

			leftHandToggle.onValueChanged.AddListener(v => OptionData.SetUseLeftHandMode(v));
			volumeSlider.onValueChanged.AddListener((v) => OptionData.SetVolume(v));
			homeButton.onClick.AddListener(() => {
				PoppupManager.CloseAsync(this).ContinueWith(()=>GameManager.Instance.ChangeState(GameState.Main).Forget()).Forget();
			});
			resumeButton.onClick.AddListener(()=> { Close(); });
		}
	}
}