using UnityEngine;

public class PlayerRoundResultBlackjackModel : PlayerBlackjackModel {

	[SerializeField]
	private string m_RoundResultModelID;

	public override void RegisterModelObject(object obj) {
		base.RegisterModelObject(obj);
		Game.OnSeatWin += OnSeatWin;
		Game.OnSeatLose += OnSeatLose;
		Game.OnSeatBust += OnSeatBust;
		Game.OnDealerBust += OnDealerBust;
	}

	public override void DeregisterModelObject(object obj) {
		Game.OnSeatWin -= OnSeatWin;
		Game.OnSeatLose -= OnSeatLose;
		Game.OnSeatBust -= OnSeatBust;
		base.DeregisterModelObject(obj);
	}

	private void OnSeatWin(Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints) {
		string resultText = string.Format("Player wins with {0} point(s)", seatHighestTotalPoints);
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultModelID, resultText, "");
	}

	private void OnSeatLose(Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints) {
		string resultText = string.Format("Player loses with {0} point(s)", seatHighestTotalPoints);
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultModelID, resultText, "");
	}

	private void OnSeatBust(Seat seat, int seatHighestTotalPoints) {
		string resultText = string.Format("Player busts with {0} point(s)", seatHighestTotalPoints);
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultModelID, resultText, "");
	}

	private void OnDealerBust(Seat seat) {
		string resultText = string.Format("Dealer busted, player wins");
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultModelID, resultText, "");
	}

}
