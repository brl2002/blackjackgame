using UnityEngine;

public class PlayerBlackjackModel : BlackjackModel {

	[SerializeField]
	private string m_BetAmountModelID;

	[SerializeField]
	private string m_CashInHandModelID;

	private Seat m_Seat;

	public override void RegisterModelObject(object obj) {
		m_Seat = (Seat)obj;
		m_Seat.OnBetAmountChanged += OnBetAmountChanged;
		m_Seat.OnCashInHandChanged += OnCashInHandChanged;
	}

	/// <summary>
	/// Make sure to call parent DeregisterModelObject call very last
	/// </summary>
	/// <param name="obj"></param>
	public override void DeregisterModelObject(object obj) {
		m_Seat.OnBetAmountChanged -= OnBetAmountChanged;
		m_Seat.OnCashInHandChanged -= OnCashInHandChanged;
		m_Seat = null;
	}

	private void OnBetAmountChanged(int newAmout, int previousAmount) {
		m_OnBlackjackModelChanged?.Invoke(m_BetAmountModelID, newAmout, previousAmount);
	}

	private void OnCashInHandChanged(int newCashInHand, int previousCashInHand) {
		m_OnBlackjackModelChanged?.Invoke(m_CashInHandModelID, newCashInHand, previousCashInHand);
	}

}
