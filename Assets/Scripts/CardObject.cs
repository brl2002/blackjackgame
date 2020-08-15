using UnityEngine;

public class CardObject : MonoBehaviour {

	public enum State {
		FRONT,
		BACK
	}

	#region Fields

	private SpriteRenderer m_SpriteRenderer;

	private Sprite m_CardImage;

	private State m_State = State.FRONT;

	#endregion

	#region Private Methods

	private void Awake() {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	#endregion

	#region Public Methods

	public void SetImage(Sprite sprite) {
		m_CardImage = sprite;
		m_SpriteRenderer.sprite = m_CardImage;
	}

	public void ShowCard() {
		m_SpriteRenderer.sprite = m_CardImage;
		m_State = State.FRONT;
	}

	public void HideCard() {
		m_SpriteRenderer.sprite = Game.Instance.CardPool.GetBackImage();
		m_State = State.BACK;
	}

	public void Flip() {
		if (m_State == State.FRONT) {
			HideCard();
		} else {
			ShowCard();
		}
	}

	#endregion

}
