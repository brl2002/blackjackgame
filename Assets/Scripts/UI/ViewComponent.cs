using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#region ViewComponent CustomYieldInstructions

public class WaitForViewComponentToHide : CustomYieldInstruction {

	private ViewComponent m_ViewComponent;

	public override bool keepWaiting {
		get {
			return m_ViewComponent.IsHiding;
		}
	}

	public WaitForViewComponentToHide(ViewComponent viewComponent) {
		m_ViewComponent = viewComponent;
	}

}

public class WaitForViewComponentToShow : CustomYieldInstruction {

	private ViewComponent m_ViewComponent;

	public override bool keepWaiting {
		get {
			return m_ViewComponent.IsShowing;
		}
	}

	public WaitForViewComponentToShow(ViewComponent viewComponent) {
		m_ViewComponent = viewComponent;
	}

}

#endregion

public class ViewComponent : MonoBehaviour {

	public delegate void ViewComponentEvent(object obj);

	[SerializeField]
	protected string m_ID;

	protected bool m_IsInitializationComplete = false;

	protected bool m_IsHiding = false;

	protected bool m_IsShowing = false;

	public string ID {
		get {
			return m_ID;
		}
	}

	public bool IsInitializationComplete {
		get {
			return m_IsInitializationComplete;
		}
	}

	public bool IsHiding {
		get {
			return m_IsHiding;
		}
	}

	public bool IsShowing {
		get {
			return m_IsShowing;
		}
	}

	protected virtual void Awake() {
	}

	public virtual void Register(ViewComponentEvent onViewComponentEvent) {
	}

	public virtual void Hide() {
		StartCoroutine(HideCoroutine());
	}

	protected virtual IEnumerator HideCoroutine() {
		yield return null;
		m_IsHiding = true;
		m_IsHiding = false;
	}

	public virtual void Show() {
		StartCoroutine(ShowCoroutine());
	}

	protected virtual IEnumerator ShowCoroutine() {
		yield return null;
		m_IsShowing = true;
		m_IsShowing = false;
	}

	public virtual void HideImmediate() {
	}

	public virtual void ShowImmediate() {
	}

	public virtual void UpdateComponent(object newValue, object previousValue) {
	}

}
