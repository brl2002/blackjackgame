using UnityEngine;
using System.Collections;

public class BlackjackController : MonoBehaviour {

	[SerializeField]
	private BlackjackView[] m_BlackjackViews;

	protected virtual void Awake() {
		StartCoroutine(WaitForAllViewsInitializedRoutine());
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

	private IEnumerator WaitForAllViewsInitializedRoutine() {
		while (!IsAllViewsInitialized) {
			yield return null;
		}
		OnAllViewsInitialized(m_BlackjackViews);
	}

	protected virtual void OnAllViewsInitialized(BlackjackView[] blackjackViews) {
	}

}
