using UnityEngine;
using UnityEngine.UI;

public class TextViewComponent : ViewComponent {

	private Text m_Text;

	protected override void Awake() {
		m_Text = GetComponent<Text>();
		m_IsInitializationComplete = true;
	}

	public override void UpdateComponent(object newValue, object previousValue) {
		m_Text.text = newValue.ToString();
	}

}
