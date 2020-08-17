using System.Collections;
using UnityEngine;

#region BlackjackView CustomYieldInstructions

public class WaitForViewComponentsToHide : CustomYieldInstruction {

	private BlackjackView m_BlackjackView;

	public override bool keepWaiting {
		get {
			return m_BlackjackView.IsViewComponentsHiding;
		}
	}

	public WaitForViewComponentsToHide(BlackjackView blackjackView) {
		m_BlackjackView = blackjackView;
	}

}

public class WaitForViewComponentsToShow : CustomYieldInstruction {

	private BlackjackView m_BlackjackView;

	public override bool keepWaiting {
		get {
			return m_BlackjackView.IsViewComponentsShowing;
		}
	}

	public WaitForViewComponentsToShow(BlackjackView blackjackView) {
		m_BlackjackView = blackjackView;
	}

}

#endregion

public class BlackjackView : MonoBehaviour {

	public delegate void BlackjackViewEvent(object obj);

	[SerializeField]
	protected string m_ID;

	[SerializeField]
	private BlackjackModel m_BlackjackModelPrefab;

	private ViewComponent[] m_ViewComponents;

	protected BlackjackModel m_BlackjackModel;

	protected virtual void Awake() {
		m_ViewComponents = GetComponentsInChildren<ViewComponent>();
		m_BlackjackModel = Instantiate(m_BlackjackModelPrefab) as BlackjackModel;
		m_BlackjackModel.transform.SetParent(this.transform);
	}

	protected virtual void OnViewComponentEventFired(object obj) {
	}

	public string ID {
		get {
			return m_ID;
		}
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

	public bool IsViewComponentsHiding {
		get {
			if (m_ViewComponents == null) {
				return false;
			}
			foreach (var viewComp in m_ViewComponents) {
				if (viewComp.IsHiding) {
					return true;
				}
			}
			return false;
		}
	}

	public bool IsViewComponentsShowing {
		get {
			if (m_ViewComponents == null) {
				return false;
			}
			foreach (var viewComp in m_ViewComponents) {
				if (viewComp.IsShowing) {
					return true;
				}
			}
			return false;
		}
	}

	public virtual void RegisterForViewComponentEvent(ViewComponent.ViewComponentEvent viewComponentEvent) {
		foreach (var viewComp in m_ViewComponents) {
			viewComp.Register((obj) => {
				viewComponentEvent(obj);
			});
		}
	}

	public virtual void RegisterViewModelObject(object obj) {
		m_BlackjackModel.RegisterOnBlackjackModelChangedEvent((id, newValue, previousValue) => {
			foreach (var viewComp in m_ViewComponents) {
				if (viewComp.ID.Equals(id)) {
					viewComp.UpdateComponent(newValue, previousValue);
				}
			}
		});
	}

	public virtual void Hide(BlackjackViewEvent onHideBlackjackViewCompletion) {
		StartCoroutine(HideCoroutine(onHideBlackjackViewCompletion));
	}

	protected virtual IEnumerator HideCoroutine(BlackjackViewEvent onHideBlackjackViewCompletion) {
		foreach (var viewComp in m_ViewComponents) {
			viewComp.Hide();
		}
		yield return new WaitForViewComponentsToHide(this);
		onHideBlackjackViewCompletion?.Invoke(null);
	}

	public virtual void Show(BlackjackViewEvent onShowBlackjackViewCompletion) {
		StartCoroutine(ShowCoroutine(onShowBlackjackViewCompletion));
	}

	protected virtual IEnumerator ShowCoroutine(BlackjackViewEvent onShowBlackjackViewCompletion) {
		foreach (var viewComp in m_ViewComponents) {
			viewComp.Show();
		}
		yield return new WaitForViewComponentsToShow(this);
		onShowBlackjackViewCompletion?.Invoke(null);
	}

	public virtual void HideImmediate() {
		foreach (var viewComp in m_ViewComponents) {
			viewComp.HideImmediate();
		}
	}

	public virtual void ShowImmediate() {
		foreach (var viewComp in m_ViewComponents) {
			viewComp.ShowImmediate();
		}
	}

}
