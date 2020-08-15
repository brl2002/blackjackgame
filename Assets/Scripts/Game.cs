using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	#region Serialized Fields

	[SerializeField]
	private Seat m_SeatPrefab;

	[SerializeField]
	private CardPool m_CardPoolPrefab;

	[SerializeField]
	private Transform[] m_Positions;

	#endregion

	#region Fields

	private int m_PositionIndex = 0;

	private List<Seat> m_Seats = new List<Seat>();

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
		m_CardPool = Instantiate(m_CardPoolPrefab);
		m_DealerSeat = AddSeat(Seat.Type.DEALER);
		AddSeat(Seat.Type.PLAYER);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			DealFirstCards();
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			Hit(m_Seats[1]);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			Stand(m_Seats[1]);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			DoubleDown(m_Seats[1]);
		}
	}

	#endregion

	#region public Methods

	public Seat AddSeat(Seat.Type type) {
		Seat seat = Instantiate(m_SeatPrefab) as Seat;
		seat.SetPosition(m_Positions[m_PositionIndex++].position);
		seat.SetType(type);
		m_Seats.Add(seat);
		return seat;
	}

	public void DealFirstCards() {
		for (int i = 0; i < 2; i++) {
			foreach (var seat in m_Seats) {
				seat.DealCard(m_CardPool.GetCard());
			}
		}
		foreach (var seat in m_Seats) {
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

}
