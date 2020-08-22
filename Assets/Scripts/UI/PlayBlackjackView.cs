using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayBlackjackView : BlackjackView {

	private CanvasGroup m_CanvasGroup;

	protected override void Awake() {
		base.Awake();
		m_CanvasGroup = GetComponent<CanvasGroup>();
	}

	protected override void OnViewComponentEventFired(object obj) {
		
	}

	public override void RegisterViewModelObject(object obj) {
		base.RegisterViewModelObject(obj);
		m_BlackjackModel.RegisterModelObject(obj);
	}

	protected override IEnumerator HideCoroutine(BlackjackViewEvent onHideBlackjackViewCompletion) {
		yield return StartCoroutine(base.HideCoroutine(onHideBlackjackViewCompletion));
		m_CanvasGroup.alpha = 0;
		m_CanvasGroup.interactable = false;
		m_CanvasGroup.blocksRaycasts = false;
		onHideBlackjackViewCompletion?.Invoke(null);
	}

	protected override IEnumerator ShowCoroutine(BlackjackViewEvent onShowBlackjackViewCompletion) {
		yield return StartCoroutine(base.ShowCoroutine(onShowBlackjackViewCompletion));
		m_CanvasGroup.alpha = 1;
		m_CanvasGroup.interactable = true;
		m_CanvasGroup.blocksRaycasts = true;
		onShowBlackjackViewCompletion?.Invoke(null);
	}

	public override void HideImmediate() {
		m_CanvasGroup.alpha = 0;
		m_CanvasGroup.interactable = false;
		m_CanvasGroup.blocksRaycasts = false;
	}

	public override void ShowImmediate() {
		m_CanvasGroup.alpha = 1;
		m_CanvasGroup.interactable = true;
		m_CanvasGroup.blocksRaycasts = true;
	}

}
