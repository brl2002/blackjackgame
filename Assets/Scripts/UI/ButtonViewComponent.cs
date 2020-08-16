using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonViewComponent : ViewComponent {

	[SerializeField]
	private string m_ButtonId;

	private Button m_Button;

	protected override void Awake() {
		m_Button = GetComponent<Button>();
		m_IsInitializationComplete = true;
	}

	public override void Register(ViewComponentEvent viewComponentEvent) {
		m_Button.onClick.AddListener(() => {
			viewComponentEvent(m_ButtonId);
		});
	}

}
