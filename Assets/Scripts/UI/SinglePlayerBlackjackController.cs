using UnityEngine;
using System;

public class SinglePlayerBlackjackController : BlackjackController {

	private const string BUTTON_VIEW_COMPONENT_ID_PREFIX = "ButtonViewComponent";

	private const string TEXT_VIEW_COMPONENT_ID_PREFIX = "Text";

	protected override void Awake() {
		base.Awake();
		Game.OnSeatWin += OnSeatWin;
		Game.OnSeatLose += OnSeatLose;
		Game.OnSeatBust += OnSeatBust;
		Game.OnDealerBust += OnDealerBust;
		Game.OnRoundComplete += OnRoundComplete;
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
		GoToView("BlackjackView.PlayBlackjackView", () => {
			Seat seat = Game.Instance.JoinSeat();
			// We want to first register model object
			foreach (var view in m_BlackjackViews) {
				// Hacky solution for registering target seat
				view.RegisterViewModelObject(Game.Instance.GetSeat(1));
			}
			seat.ResetBettingAmount();
			seat.HandOutCash(BlackjackRules.Instance.PlayerStartingCashAmount);
			Game.Instance.StartGame();
			Debug.Log("Player Joined");
		});
	}

	private void OnSeatWin(Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints) {

	}

	private void OnSeatLose(Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints) {

	}

	private void OnSeatBust(Seat seat, int seatHighestTotalPoints) {

	}

	private void OnDealerBust(Seat seat) {

	}

	private void OnRoundComplete(Seat seat) {

	}

}
