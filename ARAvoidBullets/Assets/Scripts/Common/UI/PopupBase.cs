using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System;
using Unity.Collections;
using UnityEngine.UI;

namespace Almond
{
	public class PopupBase : MonoBehaviour, IPoppup
	{
		[SerializeField] private string popupName; 

		[Serializable]
		public struct PopupAnimationParam
		{
			public enum AnimationType
			{
				Scale,
				worldPosition,
				localPosition,
			}

			public AnimationType animationType;
			public Vector3 startValue;
			public Vector3 endValue;
			public Ease animationEase;
			public float animationTime;
		}

		[Header("Animation")]
		[SerializeField] private PopupAnimationParam openAnimation;
		[SerializeField] private PopupAnimationParam closeAnimation;
		[SerializeField] private Transform root;

		[SerializeField] private Button closeButton;

		protected virtual void Awake()
		{
			popupName = string.IsNullOrEmpty(popupName) ? name : popupName;
			root ??= transform;
			if(closeButton)
			{
				closeButton.onClick.AddListener(Close);
			}
		}

		public virtual void Close()
		{
			PoppupManager.Close(this);
		}

		public bool Open { get; set; }
		public string Key => popupName;
		public async UniTask OpenAnimation()
		{
			var seq = DOTween.Sequence();
			seq.Append(
			openAnimation.animationType switch
			{
				PopupAnimationParam.AnimationType.Scale =>
					seq.Append(root.DOScale(openAnimation.startValue, 0)).
					Append(root.DOScale(openAnimation.endValue, openAnimation.animationTime)),
				PopupAnimationParam.AnimationType.worldPosition =>
					seq.Append(root.DOMove(openAnimation.startValue, 0)).
					Append(root.DOMove(openAnimation.endValue, openAnimation.animationTime)),
				PopupAnimationParam.AnimationType.localPosition =>
					seq.Append(root.DOLocalMove(openAnimation.startValue, 0)).
					Append(root.DOLocalMove(openAnimation.endValue, openAnimation.animationTime)),
				_ => throw new NotImplementedException(),
			});
			await seq.AsyncWaitForCompletion();
		}
		public async UniTask CloseAnimation()
		{
			var seq = DOTween.Sequence();
			seq.Append(
			closeAnimation.animationType switch
			{
				PopupAnimationParam.AnimationType.Scale =>
					seq.Append(root.DOScale(closeAnimation.startValue, 0)).
					Append(root.DOScale(closeAnimation.endValue, closeAnimation.animationTime)),
				PopupAnimationParam.AnimationType.worldPosition =>
					seq.Append(root.DOMove(closeAnimation.startValue, 0)).
					Append(root.DOMove(closeAnimation.endValue, closeAnimation.animationTime)),
				PopupAnimationParam.AnimationType.localPosition =>
					seq.Append(root.DOLocalMove(closeAnimation.startValue, 0)).
					Append(root.DOLocalMove(closeAnimation.endValue, closeAnimation.animationTime)),
				_ => throw new NotImplementedException(),
			});
			await seq.AsyncWaitForCompletion();
		}
	}
}