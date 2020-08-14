using UnityEngine;

public class BlackjackRules : MonoBehaviour {

	#region Monobehaviour Methods

	private void Awake() {
		m_GameSettings = Resources.Load<GameSettings>("GameSettings");
	}

	#endregion

	#region Fields

	private GameSettings m_GameSettings;

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
