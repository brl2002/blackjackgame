using UnityEngine;

public class CardObject : MonoBehaviour {

	private SpriteRenderer m_SpriteRenderer;

	private void Awake() {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void SetImage(Sprite sprite) {
		m_SpriteRenderer.sprite = sprite;
	}

}
