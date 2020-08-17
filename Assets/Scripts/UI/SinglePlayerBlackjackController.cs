using UnityEngine;
using System;

public class SinglePlayerBlackjackController : BlackjackController {

	private const string BUTTON_VIEW_COMPONENT_ID_PREFIX = "ButtonViewComponent";

	private const string TEXT_VIEW_COMPONENT_ID_PREFIX = "Text";

	protected override void Awake() {
		base.Awake();
	}

	protected override void OnInitializationComplete() {
		foreach (var view in m_BlackjackViews) {
			view.RegisterForViewComponentEvent((obj) => {
				string stringObj = (string)obj;
				if (stringObj != null) {
					try {
						string[] tokens = stringObj.Split('.');
						string viewStr = tokens[0];
						string typeStr = tokens[1];
						string componentStr = tokens[2];
						if (typeStr == BUTTON_VIEW_COMPONENT_ID_PREFIX) {
							switch (componentStr) {
								case "Hit":
									OnHit();
									break;
								case "Stand":
									OnStand();
									break;
								case "DoubleDown":
									OnDoubleDown();
									break;
								case "Join":
									OnJoin();
									break;
							}
						}
					} catch (Exception e) {
						Debug.LogException(e);
					}
				}
			});
		}
	}

	private void OnHit() {
		Game.Instance.Hit(Game.Instance.GetSeat(1));
		Debug.Log("Player Hit");
	}

	private void OnStand() {
		Game.Instance.Stand(Game.Instance.GetSeat(1));
		Debug.Log("Player Stand");
	}

	private void OnDoubleDown() {
		Game.Instance.DoubleDown(Game.Instance.GetSeat(1));
		Debug.Log("Player Double Down");
	}

	private void OnJoin() {
		Game.Instance.StartGame();
		foreach (var view in m_BlackjackViews) {
			// Hacky solution for registering target seat
			view.RegisterViewModelObject(Game.Instance.GetSeat(1));
		}
		GoToView("BlackjackView.PlayBlackjackView");
		Debug.Log("Player Join");
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.H)) {
			m_CurrenBlackjackView.Hide((obj) => {
				Debug.Log("Blackjack View OnHideComplete");
			});
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			m_CurrenBlackjackView.Show((obj) => {
				Debug.Log("Blackjack View OnShowComplete");
			});
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			GoToView("BlackjackView.PlayBlackjackView");
		}
	}

}
