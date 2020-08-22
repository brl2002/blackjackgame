using UnityEngine;

public class RoundResultBlackjackView : PlayBlackjackView {

	protected override void Awake() {
		base.Awake();
		Game.OnSeatWin += OnSeatWin;
		Game.OnSeatLose += OnSeatLose;
		Game.OnSeatBust += OnSeatBust;
		Game.OnDealerBust += OnDealerBust;
	}

	private void OnDestroy() {
		Game.OnSeatWin -= OnSeatWin;
		Game.OnSeatLose -= OnSeatLose;
		Game.OnSeatBust -= OnSeatBust;
		Game.OnDealerBust -= OnDealerBust;
	}

	private void OnSeatWin(Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints) {
		
	}

	private void OnSeatLose(Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints) {
		
	}

	private void OnSeatBust(Seat seat, int seatHighestTotalPoints) {
		
	}

	private void OnDealerBust(Seat seat) {
		
	}

	public override void Hide(BlackjackViewEvent onHideBlackjackViewCompletion) {
		base.Hide(onHideBlackjackViewCompletion);

	}

	public override void Show(BlackjackViewEvent onShowBlackjackViewCompletion) {
		base.Show(onShowBlackjackViewCompletion);

	}

	public override void HideImmediate() {
		base.HideImmediate();

	}

	public override void ShowImmediate() {
		base.ShowImmediate();

	}

}
