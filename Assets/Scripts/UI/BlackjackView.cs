using UnityEngine;

public class BlackjackView : MonoBehaviour {

	private ViewComponent[] m_ViewComponents;

	protected virtual void Awake() {
		m_ViewComponents = GetComponentsInChildren<ViewComponent>();
	}

	protected virtual void OnViewComponentEventFired(object obj) {
	}

	public bool IsInitializationComplete {
		get {
			if (m_ViewComponents == null) {
				return false;
			}
			foreach (var viewComp in m_ViewComponents) {
				if (!viewComp.IsInitializationComplete) {
					return false;
				}
			}
			return true;
		}
	}

	public virtual void RegisterForViewComponentEvent(ViewComponent.ViewComponentEvent viewComponentEvent) {
		foreach (var viewComp in m_ViewComponents) {
			viewComp.Register((obj) => {
				viewComponentEvent(obj);
			});
		}
	}

}
