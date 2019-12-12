using UnityEngine;

/**
 * Bullet class controlling bullet rigid body
 */
public class Bullet : MonoBehaviour {
	// Private variables
	private Rigidbody2D _bullet;

	// Public variables
	public bool facingLeft = true;
	public int speed;

	private void Start() {
		if (facingLeft) // if facing left, move left
			speed *= -1;

		_bullet = gameObject.GetComponent<Rigidbody2D>();
		_bullet.velocity = new Vector2(speed, 0);
	}

	private void OnCollisionEnter2D(Collision2D colObj) {
		Destroy(gameObject); // Destroy object on collision
	}
}
