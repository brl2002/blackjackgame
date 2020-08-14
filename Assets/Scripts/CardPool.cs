using UnityEngine;
using System.Collections.Generic;

public class CardPool : MonoBehaviour {

	#region Serialized Fields

	[SerializeField]
	private Sprite[] m_CardSprites;

	#endregion

	#region Private Fields

	private List<Card> m_Cards = new List<Card>();

	#endregion

	#region Monobehvaiour Methods

	private void Awake() {
		InitializePoolOfCards();
		ShuffleCards();
	}

	#endregion

	#region Private Methods

	private void InitializePoolOfCards() {
		for (int i = 0; i < 8; i++) {
			foreach (var cardEnum in System.Enum.GetValues(typeof(CardType))) {
				Card card = new Card() {
					cardType = (CardType)cardEnum
				};
				m_Cards.Add(card);
			}
		}
	}

	#endregion

	#region Public Methods

	public void ShuffleCards() {
		int n = m_Cards.Count;
		while (n > 1) {
			n--;
			int i = Random.Range(0, n + 1);
			Card card = m_Cards[i];
			m_Cards[i] = m_Cards[n];
			m_Cards[n] = card;
		}
	}

	public Card GetCard() {
		Card card = m_Cards[0];
		m_Cards.RemoveAt(0);
		return card;
	}

	#endregion

}
