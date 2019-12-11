using UnityEngine;

public class Bullet : MonoBehaviour {
	private Rigidbody2D _bullet;

	public bool facingLeft = true;
	public int speed;

	// Start is called before the first frame update
	private void Start() {
		if (facingLeft)
			speed *= -1;

		_bullet = gameObject.GetComponent<Rigidbody2D>();
		_bullet.velocity = new Vector2(speed, 0);
	}

	private void OnCollisionEnter2D(Collision2D colObj) {
		Destroy(gameObject);
	}
}
