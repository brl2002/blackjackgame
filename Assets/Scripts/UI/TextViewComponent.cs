using UnityEngine;
using UnityEngine.UI;

public class TextViewComponent : ViewComponent {

	private Text m_Text;

	protected override void Awake() {
		m_Text = GetComponent<Text>();
		m_IsInitializationComplete = true;
	}

	public override void UpdateComponent(object newValue, object previousValue) {
		// TO-DO: Updating text here is somewhat hard coded with updating only cash in mind, will need to implement a better logic for this
		m_Text.text = string.Format("${0}", newValue.ToString());
	}

}
