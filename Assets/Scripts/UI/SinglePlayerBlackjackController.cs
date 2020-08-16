using UnityEngine;

public class SinglePlayerBlackjackController : BlackjackController {

	private const string BUTTON_ID_PREFIX = "Button";

	private const string TEXT_ID_PREFIX = "Text";

	protected override void Awake() {
		base.Awake();
	}

	protected override void OnAllViewsInitialized(BlackjackView[] blackjackViews) {
		foreach (var view in blackjackViews) {
			view.RegisterForViewComponentEvent((obj) => {
				string stringObj = (string)obj;
				if (stringObj != null) {
					if (stringObj.StartsWith(BUTTON_ID_PREFIX)) {
						Debug.LogFormat("Button Event Fired: {0}", stringObj);
					}
				}
			});
		}
	}

}
