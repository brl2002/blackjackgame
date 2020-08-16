using UnityEngine;

public class PlayBlackjackView : BlackjackView {

	protected override void OnViewComponentEventFired(object obj) {
		Debug.LogFormat("OnViewComponentEventFired: {0}", obj);
	}

}
