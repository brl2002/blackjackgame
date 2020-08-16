using UnityEngine;
using UnityEngine.Events;

public class ViewComponent : MonoBehaviour {

	public delegate void ViewComponentEvent(object obj);

	protected bool m_IsInitializationComplete = false;

	public bool IsInitializationComplete {
		get {
			return m_IsInitializationComplete;
		}
	}

	protected virtual void Awake() {
	}

	public virtual void Register(ViewComponentEvent viewComponentEvent) {
	}

}
