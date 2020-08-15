using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	#region Serialized Fields

	[SerializeField]
	private Seat m_SeatPrefab;

	[SerializeField]
	private CardPool m_CardPoolPrefab;

	#endregion

	#region Fields

	private Seat[] m_Seats;

	private int m_TurnCount = 0;

	private List<Seat> m_TakenSeats = new List<Seat>();

	private Seat m_DealerSeat;

	private CardPool m_CardPool;

	#endregion

	#region Properties

	public CardPool CardPool {
		get {
			return m_CardPool;
		}
	}

	#endregion

	#region Monobeahviour Methods

	private void Awake() {
		m_Seats = GetComponentsInChildren<Seat>();
		m_CardPool = Instantiate(m_CardPoolPrefab);
		m_DealerSeat = AddSeat(Seat.Type.DEALER);
		AddSeat(Seat.Type.PLAYER);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			DealFirstCards();
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			Hit(m_TakenSeats[1]);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			Stand(m_TakenSeats[1]);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			DoubleDown(m_TakenSeats[1]);
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			m_DealerSeat.ShowCards();
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			m_DealerSeat.ShowDefault();
		}
	}

	#endregion

	#region Helper Methods

	private bool SeatIsTaken(Seat seat) {
		foreach (var otherSeat in m_TakenSeats) {
			if (seat == otherSeat) {
				return true;
			}
		}
		return false;
	}

	private int GetNextAvailableSeatTransformIndex() {
		for (int i = 0; i < m_Seats.Length; i++) {
			if (!SeatIsTaken(m_Seats[i])) {
				return i;
			}
		}
		return -1;
	}

	#endregion

	#region public Methods

	public Seat AddSeat(Seat.Type type) {
		int seatIndex = GetNextAvailableSeatTransformIndex();
		if (seatIndex > -1) {
			Seat seat = m_Seats[seatIndex];
			seat.SetType(type);
			m_TakenSeats.Add(seat);
			return seat;
		}
		return null;
	}

	public void DealFirstCards() {
		for (int i = 0; i < 2; i++) {
			foreach (var seat in m_TakenSeats) {
				seat.DealCard(m_CardPool.GetCard());
			}
		}
		foreach (var seat in m_TakenSeats) {
			Debug.LogFormat("{0} Score: {1}", seat.GetSeatType(), seat.GetHighestTotalScore());
		}
	}

	public void Hit(Seat seat) {
		seat.DealCard(m_CardPool.GetCard());
		Debug.LogFormat("{0} Score: {1}", seat.GetSeatType(), seat.GetHighestTotalScore());
	}

	public void Stand(Seat seat) {
		
	}

	public void DoubleDown(Seat seat) {
		seat.DealCard(m_CardPool.GetCard());
		Debug.LogFormat("{0} Score: {1}", seat.GetSeatType(), seat.GetHighestTotalScore());
	}

	#endregion

	#region Singleton

	private static Game s_Instance;

	public static Game Instance {
		get {
			if (s_Instance == null) {
				s_Instance = FindObjectOfType<Game>();
				s_Instance.name = "Game";
				DontDestroyOnLoad(s_Instance);
			}
			return s_Instance;
		}
	}

	#endregion

	#region Editor Functionalities

	[MenuItem("Blackjack/Debug")]
	static void TurnOnDebug() {

	}

	#endregion

}
