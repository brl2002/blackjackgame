using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {

	public enum State {
		WAITING,
		DEALING_STARTING_CARDS,
		WAITING_FOR_PLAYER,
		DEALING_PLAYER_CARD,
		DEALING_DEALER_CARDS,
		ROUND_COMPLETE
	}

	#region Events

	/// <summary>
	/// Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints
	/// </summary>
	public static Action<Seat, int, int> OnSeatWin;

	/// <summary>
	/// Seat seat, int seatHighestTotalPoints, int dealerHighestTotalPoints
	/// </summary>
	public static Action<Seat, int, int> OnSeatLose;


	/// <summary>
	/// Seat seat, int seatHighestTotalPoints
	/// </summary>
	public static Action<Seat, int> OnSeatBust;

	public static Action<Seat> OnDealerBust;

	public static Action<Seat> OnRoundComplete;

	#endregion

	#region Serialized Fields

	[SerializeField]
	private CardPool m_CardPoolPrefab;

	#endregion

	#region Fields

	private State m_State;

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
		m_State = State.WAITING;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			if (m_State == State.WAITING) {
				m_DealerSeat = AddSeat(Seat.Type.DEALER);
				Seat player = AddSeat(Seat.Type.PLAYER);
				player.HandOutCash(BlackjackRules.Instance.PlayerStartingCashAmount);
				m_State = State.DEALING_STARTING_CARDS;
				DealFirstCards();
				m_State = State.WAITING_FOR_PLAYER;
			}
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			if (m_State == State.WAITING_FOR_PLAYER) {
				m_State = State.DEALING_PLAYER_CARD;
				Hit(m_TakenSeats[1]);
				m_State = State.WAITING_FOR_PLAYER;
			}
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			if (m_State == State.WAITING_FOR_PLAYER) {
				m_State = State.DEALING_DEALER_CARDS;
				Stand(m_TakenSeats[1]);
			}
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			if (m_State == State.WAITING_FOR_PLAYER) {
				m_State = State.DEALING_PLAYER_CARD;
				DoubleDown(m_TakenSeats[1]);
				m_State = State.WAITING_FOR_PLAYER;
			}
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			m_DealerSeat.ShowCards();
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			m_DealerSeat.ShowDefault();
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			ClearTable();
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

	/// <summary>
	/// For a given seat, determine outcome of holding set of cards
	/// </summary>
	/// <returns>1 if given seat beats other seat, 0 if given seat loses to other seat, -1 if given seat busts</returns>
	private int GetOutcome(Seat seat, Seat otherSeat) {
		int seatHighestTotalScore = 0;
		bool seatBusts = GetHighestTotalScoreAndCheckBust(seat, out seatHighestTotalScore);
		int otherSeatHighestTotalScore = 0;
		bool otherSeatBusts = GetHighestTotalScoreAndCheckBust(otherSeat, out otherSeatHighestTotalScore);
		if (seatBusts) {
			return seatHighestTotalScore > otherSeatHighestTotalScore ? 1 : 0;
		}
		return -1;
	}

	/// <summary>
	/// Get highest total score for a seat and check for bust
	/// </summary>
	/// <param name="seat"></param>
	/// <param name="seatHighestTotalScore"></param>
	/// <returns>true for bust</returns>
	private bool GetHighestTotalScoreAndCheckBust(Seat seat, out int seatHighestTotalScore) {
		seatHighestTotalScore = 0;
		return seat.GetHighestTotalScore(out seatHighestTotalScore);
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
			int seatHighestScore = 0;
			seat.GetHighestTotalScore(out seatHighestScore);
			Debug.LogFormat("{0} Score: {1}", seat.GetSeatType(), seatHighestScore);
		}
	}

	public void DealCard(Seat seat) {
		seat.DealCard(m_CardPool.GetCard());
		int seatHighestScore = 0;
		seat.GetHighestTotalScore(out seatHighestScore);
		Debug.LogFormat("{0} Score: {1}", seat.GetSeatType(), seatHighestScore);
	}

	public void ClearTable() {
		foreach (var seat in m_TakenSeats) {
			seat.Clear();
		}
	}

	public void CompleteRound() {
		ClearTable();
		m_State = State.ROUND_COMPLETE;
		if (OnRoundComplete != null) {
			foreach (var seat in m_TakenSeats) {
				OnRoundComplete(seat);
			}
		}
		Debug.Log("Round Complete");
		m_State = State.DEALING_STARTING_CARDS;
		DealFirstCards();
		m_State = State.WAITING_FOR_PLAYER;
	}

	public void Hit(Seat seat) {
		DealCard(seat);
		int seatHighestTotalScore = 0;
		if (GetHighestTotalScoreAndCheckBust(seat, out seatHighestTotalScore)) {
			Debug.LogFormat("Seat {0} {1} busts with score {2}", seat.GetSeatType(), seat.name, seatHighestTotalScore);
			if (OnSeatBust != null) {
				OnSeatBust(seat, seatHighestTotalScore);
			}
			// Decrement player cash by current bet amount
			seat.RemoveCash(seat.BettingAmount);
			CompleteRound();
		}
	}

	public void Stand(Seat seat) {
		// now dealer's turn so deal cards until 17 or higher to stand or busts
		// this logic would not work with a table with multiple players
		int dealerHighestScore = 0;
		m_DealerSeat.GetHighestTotalScore(out dealerHighestScore);
		while (dealerHighestScore < 17) {
			m_DealerSeat.DealCard(m_CardPool.GetCard());
			m_DealerSeat.GetHighestTotalScore(out dealerHighestScore);
		}
		m_DealerSeat.ShowCards();
		Debug.LogFormat("{0} Score: {1}", m_DealerSeat.GetSeatType(), dealerHighestScore);
		int seatHighestScore = 0;
		seat.GetHighestTotalScore(out seatHighestScore);
		if (dealerHighestScore > 21) {
			Debug.LogFormat("Dealer {0} {1} busts with score {2}", m_DealerSeat.GetSeatType(), m_DealerSeat.name, dealerHighestScore);
			if (OnDealerBust != null) {
				OnDealerBust(seat);
			}
			// Increment player cash by current bet amount
			seat.HandOutCash(seat.BettingAmount);
			Debug.LogFormat("Seat {0} {1} wins with score {2}", seat.GetSeatType(), seat.name, seatHighestScore);
		} else {
			if (seatHighestScore > dealerHighestScore) {
				// Increment player cash by current bet amount
				seat.HandOutCash(seat.BettingAmount);
				if (OnSeatWin != null) {
					OnSeatWin(seat, seatHighestScore, dealerHighestScore);
				}
				Debug.LogFormat("Seat {0} {1} wins with score {2}", seat.GetSeatType(), seat.name, seatHighestScore);
			} else {
				// Decrement player cash by current bet amount
				seat.RemoveCash(seat.BettingAmount);
				if (OnSeatLose != null) {
					OnSeatLose(seat, seatHighestScore, dealerHighestScore);
				}
				Debug.LogFormat("Seat {0} {1} loses with score {2}", seat.GetSeatType(), seat.name, seatHighestScore);
			}
		}
		CompleteRound();
	}

	public void DoubleDown(Seat seat) {
		// Double bet amount
		seat.DoubleBettingAmount();
		// First implement a rule where doubling down will mean that it will be the last card that player gets
		Hit(seat);
		Stand(seat);
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
