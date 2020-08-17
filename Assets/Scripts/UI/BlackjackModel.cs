using UnityEngine;

public class BlackjackModel : MonoBehaviour {

	public delegate void OnBlackjackModelChanged(string id, object newValue, object previousValue);

	protected OnBlackjackModelChanged m_OnBlackjackModelChanged;

	public void RegisterOnBlackjackModelChangedEvent(OnBlackjackModelChanged onBlackjackModelChanged) {
		m_OnBlackjackModelChanged = onBlackjackModelChanged;
	}

	public virtual void RegisterModelObject(object obj) {
	}

	public virtual void DeregisterModelObject(object obj) {
	}

}
