using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	[SerializeField]
	private Seat m_SeatPrefab;

	[SerializeField]
	private Transform[] m_Positions;

	private List<Seat> m_Seats = new List<Seat>();

	private void Awake() {
		m_Seats.Add(Instantiate(m_SeatPrefab));
	}

}
