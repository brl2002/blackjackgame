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
		m_CanvasGroup.alpha = 0;
		m_CanvasGroup.interactable = false;
		onHideBlackjackViewCompletion?.Invoke(null);
		yield return null;
	}

	protected override IEnumerator ShowCoroutine(BlackjackViewEvent onShowBlackjackViewCompletion) {
		m_CanvasGroup.alpha = 1;
		m_CanvasGroup.interactable = true;
		onShowBlackjackViewCompletion?.Invoke(null);
		yield return null;
	}

	public override void HideImmediate() {
		m_CanvasGroup.alpha = 0;
		m_CanvasGroup.interactable = false;
	}

	public override void ShowImmediate() {
		m_CanvasGroup.alpha = 1;
		m_CanvasGroup.interactable = true;
	}

}
