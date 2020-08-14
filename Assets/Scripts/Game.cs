using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	[SerializeField]
	private Seat m_SeatPrefab;

	[SerializeField]
	private CardPool m_CardPoolPrefab;

	[SerializeField]
	private Transform[] m_Positions;

	private int m_PositionIndex = 0;

	private List<Seat> m_Seats = new List<Seat>();

	private Seat m_DealerSeat;

	private CardPool m_CardPool;

	private void Awake() {
		m_CardPool = Instantiate(m_CardPoolPrefab);
		m_DealerSeat = AddSeat(Seat.Type.DEALER);
		m_DealerSeat.DealCard(m_CardPool.GetCard());
	}

	private Seat AddSeat(Seat.Type type) {
		Seat seat = Instantiate(m_SeatPrefab) as Seat;
		seat.SetPosition(m_Positions[m_PositionIndex++].position);
		seat.SetType(type);
		m_Seats.Add(seat);
		return seat;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			m_DealerSeat.DealCard(m_CardPool.GetCard());
		}
	}

	#region Singleton

	private static Game s_Instance;

	public static Game Instance {
		get {
			if (s_Instance == null) {
				s_Instance = new GameObject().AddComponent<Game>();
				s_Instance.name = "Game";
				DontDestroyOnLoad(s_Instance);
			}
			return s_Instance;
		}
	}

	#endregion

}
