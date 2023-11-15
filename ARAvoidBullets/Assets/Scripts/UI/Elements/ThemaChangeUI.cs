using UnityEngine;
using Almond;
using UnityEngine.UI;
using TMPro;

namespace ARAvoid
{
	public class ThemaChangeUI : MonoBehaviour,IThemaUI
	{
		[SerializeField] private Button prev;
		[SerializeField] private Button next;
		[SerializeField] private TextMeshProUGUI text;

		private void OnEnable()
		{
			prev.onClick.AddListener(()=>ThemaManager.Inst.SetThema(ThemaManager.Inst.CurrentThema.Prev(), false));
			next.onClick.AddListener(() => ThemaManager.Inst.SetThema(ThemaManager.Inst.CurrentThema.Next(), false));
			ThemaManager.Inst.AddListener(ApplyThema);
			ApplyThema(ThemaManager.Inst.CurrentThema, true);
		}
		private void OnDisable()
		{
			prev.onClick.RemoveAllListeners();
			next.onClick.RemoveAllListeners();
			ThemaManager.Inst.RemoveListener(ApplyThema);
		}
		public void ApplyThema(Thema newThema, bool Immediate)
		{
			text.text = newThema.ToSentence();
		}
	}
}