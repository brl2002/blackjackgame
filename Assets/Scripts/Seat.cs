using UnityEngine;
using System.Collections.Generic;

public class Seat : MonoBehaviour {

	public enum Type {
		PLAYER,
		DEALER
	}

	#region Fields

	private Type m_Type;

	private List<Card> m_CardsInHand = new List<Card>();

	private CardObject m_CardObjectPrefab;

	private List<CardObject> m_CardObjects = new List<CardObject>();

	#endregion

	#region Monobehaviour Methods

	private void Awake() {
		m_CardObjectPrefab = Resources.Load<GameObject>("CardObject").GetComponent<CardObject>();
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

	#endregion

	#region Highest Score W. Recursive Strategy

	private int m_HighestSum = 0;

	private List<int> m_TotalScores = new List<int>();

	public int GetHighestTotalScore() {
		// Default value set to -1 as bust
		int highestScore = -1;
		m_TotalScores.Clear();
		GetAllTotalScoresRecursivelyImpl(0, 0);
		foreach (var score in m_TotalScores) {
			if (score < 22 && score > highestScore) {
				highestScore = score;
			}
		}
		return highestScore;
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
