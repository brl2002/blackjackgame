using UnityEngine;
using System.Collections;

public class BlackjackController : MonoBehaviour {

	[SerializeField]
	protected BlackjackView[] m_BlackjackViews;

	[SerializeField]
	private string m_StartingBlackjackViewID;

	protected BlackjackView m_CurrenBlackjackView;

	protected virtual void Awake() {
		StartCoroutine(WaitForInitializationCoroutine());
	}

	private void Start() {
		foreach (var view in m_BlackjackViews) {
			if (view.ID.Equals(m_StartingBlackjackViewID)) {
				m_CurrenBlackjackView = view;
				m_CurrenBlackjackView.ShowImmediate();
			} else {
				view.HideImmediate();
			}
		}
	}

	protected bool IsAllViewsInitialized {
		get {
			if (m_BlackjackViews == null) {
				return false;
			}
			foreach (var view in m_BlackjackViews) {
				if (!view.IsInitializationComplete) {
					return false;
				}
			}
			return true;
		}
	}

	private IEnumerator WaitForInitializationCoroutine() {
		while (!IsAllViewsInitialized) {
			yield return null;
		}
		OnInitializationComplete();
	}

	protected BlackjackView FindBlackjackView(string id) {
		foreach (var view in m_BlackjackViews) {
			if (view.ID.Equals(id)) {
				return view;
			}
		}
		return null;
	}

	protected virtual void OnInitializationComplete() {
	}

	protected virtual void GoToView(string blackjackViewID) {
		BlackjackView blackjackView = FindBlackjackView(blackjackViewID);
		if (m_CurrenBlackjackView == null) {
			m_CurrenBlackjackView = blackjackView;
			return;
		}
		m_CurrenBlackjackView.Hide((obj) => {

		});
		// TO-DO: Make transition to new blackjack view
		m_CurrenBlackjackView = blackjackView;
		m_CurrenBlackjackView.Show((obj) => {

		});
	}

}
