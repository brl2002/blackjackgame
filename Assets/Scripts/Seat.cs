﻿using UnityEngine;
using System.Collections.Generic;

public class Seat : MonoBehaviour {

	public enum Type {
		PLAYER,
		DEALER
	}

	private Type m_Type;

	private Vector2 m_Position;

	private List<Card> m_CardsInHand = new List<Card>();

	private CardObject m_CardObjectPrefab;

	private List<CardObject> m_CardObjects = new List<CardObject>();

	private void Awake() {
		m_CardObjectPrefab = Resources.Load<GameObject>("CardObject").GetComponent<CardObject>();
	}

	public void SetType(Type type) {
		m_Type = type;
	}

	public void SetPosition(Vector2 pos) {
		m_Position = pos;
	}

	public void DealCard(Card card) {
		m_CardsInHand.Add(card);

		CardObject cardObject = Instantiate(m_CardObjectPrefab);
		if (m_CardsInHand.Count == 0) {
			cardObject.transform.position = m_Position;
		} else {
			Vector2 lastCardObjectPos = m_CardObjects[m_CardObjects.Count - 1].transform.position;
			cardObject.transform.position = new Vector3(lastCardObjectPos.x + 0.8f, lastCardObjectPos.y, 0);
		}
		m_CardObjects.Add(cardObject);
	}

}
