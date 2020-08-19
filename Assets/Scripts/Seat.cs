using UnityEngine;
using System.Collections.Generic;
using System;

public class Seat : MonoBehaviour {

	public enum Type {
		PLAYER,
		DEALER
	}

	#region Events

	/// <summary>
	/// int newAmount, int previousAmount
	/// </summary>
	public Action<int, int> OnBetAmountChanged;

	/// <summary>
	/// int newCashInHand, int previousCashInHand
	/// </summary>
	public Action<int, int> OnCashInHandChanged;

	public Action<int> OnWin;

	public Action<int> OnLose;

	#endregion

	#region Fields

	private Type m_Type;

	private List<Card> m_CardsInHand = new List<Card>();

	private CardObject m_CardObjectPrefab;

	private List<CardObject> m_CardObjects = new List<CardObject>();

	private int m_CashAtHand = 0;

	private int m_BetAmount = 0;

	#endregion

	#region Monobehaviour Methods

	private void Awake() {
		m_CardObjectPrefab = Resources.Load<GameObject>("CardObject").GetComponent<CardObject>();
		Clear();
	}

	#endregion

	#region Properties

	public int BettingAmount {
		get {
			return m_BetAmount;
		}
	}

	public int CashAtHand {
		get {
			return m_CashAtHand;
		}
	}

	#endregion

	#region Public Methods

	public Type GetSeatType() {
		return m_Type;
	}

	public void SetType(Type type) {
		m_Type = type;
	}

	public void DealCard(Card card) {
		m_CardsInHand.Add(card);

		CardObject cardObject = Instantiate(m_CardObjectPrefab);
		cardObject.SetImage(Game.Instance.CardPool.GetCardImage(card.cardType));
		if (m_CardObjects.Count == 0) {
			cardObject.transform.position = transform.position;
		} else {
			Vector2 lastCardObjectPos = m_CardObjects[m_CardObjects.Count - 1].transform.position;
			cardObject.transform.position = new Vector3(lastCardObjectPos.x + 0.8f, lastCardObjectPos.y, 0);

			if (m_CardObjects.Count == 1 && m_Type == Type.DEALER) {
				cardObject.Flip();
			}
		}
		m_CardObjects.Add(cardObject);
	}

	public void ShowCards() {
		foreach (var card in m_CardObjects) {
			card.ShowCard();
		}
	}

	public void ShowDefault() {
		for (int i = 0; i < m_CardObjects.Count; i++) {
			if (i == 1 && m_Type == Type.DEALER) {
				m_CardObjects[i].HideCard();
			}
		}
	}

	/// <summary>
	/// Clears all card objects and cards in hand and resets betting amount
	/// </summary>
	public void Clear() {
		for (int i = 0; i < m_CardObjects.Count; i++) {
			Destroy(m_CardObjects[i].gameObject);
			m_CardObjects[i] = null;
		}
		m_CardObjects.Clear();
		m_CardsInHand.Clear();
		ResetBettingAmount();
	}

	public void ResetBettingAmount() {
		int minBettingAmount = BlackjackRules.Instance.MinBettingAmount;
		if (OnBetAmountChanged != null) {
			OnBetAmountChanged(minBettingAmount, m_BetAmount);
		}
		m_BetAmount = BlackjackRules.Instance.MinBettingAmount;
		Debug.LogFormat("Betting Amount Has Been Reset To: {0}", m_BetAmount);
	}

	public void DoubleBettingAmount() {
		int doubledBettingAmount = m_BetAmount * 2;
		if (OnBetAmountChanged != null) {
			OnBetAmountChanged(doubledBettingAmount, m_BetAmount);
		}
		m_BetAmount = doubledBettingAmount;
		Debug.LogFormat("Betting Amount Has Been Doubled To: {0}", m_BetAmount);
	}

	public void HandOutCash(int amount) {
		int newCashAtHand = m_CashAtHand + amount;
		if (OnCashInHandChanged != null) {
			OnCashInHandChanged(newCashAtHand, m_CashAtHand);
		}
		m_CashAtHand = newCashAtHand;
		Debug.LogFormat("Cash Has Been Adjusted To: {0}", m_CashAtHand);
	}

	public void RemoveCash(int amount) {
		int newCashAtHand = m_CashAtHand - amount;
		if (OnCashInHandChanged != null) {
			OnCashInHandChanged(newCashAtHand, m_CashAtHand);
		}
		m_CashAtHand = newCashAtHand;
		Debug.LogFormat("Cash Has Been Adjusted To: {0}", m_CashAtHand);
	}

	#endregion

	#region Highest Score W. Recursive Strategy

	private int m_HighestSum = 0;

	private List<int> m_TotalScores = new List<int>();

	public bool GetHighestTotalScore(out int highestScore) {
		highestScore = 0;
		m_TotalScores.Clear();

		int bustScore = 0; // true highest score saved to keep possible bust score
		GetAllTotalScoresRecursivelyImpl(0, 0);
		foreach (var score in m_TotalScores) {
			if (score < 22 && score > highestScore) {
				highestScore = score;
			}
			if (score > bustScore) {
				bustScore = score;
			}
		}
		bool isBust = highestScore == 0 || highestScore > 21;
		if (isBust) {
			highestScore = bustScore;
		}
		return isBust;
	}

	private void GetAllTotalScoresRecursivelyImpl(int index, int totalScore) {
		if (index > m_CardsInHand.Count - 1) {
			m_TotalScores.Add(totalScore);
			return;
		}
		int score = BlackjackRules.GetScore(m_CardsInHand[index].cardType);
		int nextIndex = index + 1;
		if (score == 11) {
			GetAllTotalScoresRecursivelyImpl(nextIndex, totalScore + 11);
			GetAllTotalScoresRecursivelyImpl(nextIndex, totalScore + 1);
		} else {
			GetAllTotalScoresRecursivelyImpl(nextIndex, totalScore + score);
		}
	}

	#endregion

}
