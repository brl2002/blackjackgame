﻿using UnityEngine;

public class BlackjackRules : MonoBehaviour {

	#region Monobehaviour Methods

	private void Awake() {
		m_GameSettings = Resources.Load<GameSettings>("GameSettings");
	}

	#endregion

	#region Fields

	private GameSettings m_GameSettings;

	#endregion

	#region Properties

	public int MinBettingAmount {
		get {
			return m_GameSettings.MinBettingAmount;
		}
	}

	public int PlayerStartingCashAmount {
		get {
			return m_GameSettings.PlayerStartingCashAmount;
		}
	}

	#endregion

	#region Public Methods

	public static int GetScore(CardType cardType) {
		int index = (int)cardType;
		if (index < 36) { // in case of numeric values
			return index / 4 + 2;
		} else if (index < 48) { // in case of king, jack, queen
			return 10;
		}
		return 11;
	}

	#endregion

	#region Singleton

	private static BlackjackRules s_Instance;

	public static BlackjackRules Instance {
		get {
			if (s_Instance == null) {
				s_Instance = new GameObject().AddComponent<BlackjackRules>();
				s_Instance.name = "BlackjackRules";
				DontDestroyOnLoad(s_Instance);
			}
			return s_Instance;
		}
	}

	[RuntimeInitializeOnLoadMethod]
	static void InitializeOnLoad() {
		if (Instance) {
		}
	}

	#endregion

}
