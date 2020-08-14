using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Create GameSettings Instance")]
public class GameSettings : ScriptableObject {

	#region Properties

	public int MinBettingAmount {
		get {
			return m_MinBettingAmount;
		}
	}

	public int PlayerStartingCashAmount {
		get {
			return m_PlayerStartingCashAmount;
		}
	}

	#endregion

	#region Serialized Fields

	[SerializeField]
	private int m_MinBettingAmount;

	[SerializeField]
	private int m_PlayerStartingCashAmount;

	#endregion

}
