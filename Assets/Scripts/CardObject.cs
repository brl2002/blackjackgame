using UnityEngine;

public class CardObject : MonoBehaviour {

	public enum State {
		FRONT,
		BACK
	}

	private SpriteRenderer m_SpriteRenderer;

	private Sprite m_CardImage;

	private State m_State = State.FRONT;

	private void Awake() {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void SetImage(Sprite sprite) {
		m_CardImage = sprite;
		m_SpriteRenderer.sprite = m_CardImage;
	}

	public void Flip() {
		if (m_State == State.FRONT) {
			m_SpriteRenderer.sprite = Game.Instance.CardPool.GetBackImage();
			m_State = State.BACK;
		} else {
			m_SpriteRenderer.sprite = m_CardImage;
			m_State = State.FRONT;
		}
	}

}
