using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ButtonViewComponent : ViewComponent {

	private Button m_Button;

	protected override void Awake() {
		m_Button = GetComponent<Button>();
		m_IsInitializationComplete = true;
	}

	public override void Register(ViewComponentEvent onViewComponentEvent) {
		m_Button.onClick.AddListener(() => {
			onViewComponentEvent(m_ID);
		});
	}

}
