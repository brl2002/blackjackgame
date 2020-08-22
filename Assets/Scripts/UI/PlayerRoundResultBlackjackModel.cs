using UnityEngine;

public class PlayerRoundResultBlackjackModel : PlayerBlackjackModel {

	[SerializeField]
	private string m_RoundResultTextModelID;

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
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultTextModelID, string.Format("Player wins with {0} points", seatHighestTotalPoints), string.Empty);
	}

	private void OnSeatLose(Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints) {
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultTextModelID, string.Format("Player loses with {0} points", seatHighestTotalPoints), string.Empty);
	}

	private void OnSeatBust(Seat seat, int seatHighestTotalPoints) {
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultTextModelID, string.Format("Player busts with {0} points", seatHighestTotalPoints), string.Empty);
	}

	private void OnDealerBust(Seat seat) {
		m_OnBlackjackModelChanged?.Invoke(m_RoundResultTextModelID, "Dealer busts", string.Empty);
	}

}
