using UnityEngine;

public class Seat : MonoBehaviour {

	public enum Type {
		PLAYER,
		DEALER
	}

	private Vector2 m_Position;

	public void SetPosition(Vector2 pos) {
		m_Position = pos;
	}

}
