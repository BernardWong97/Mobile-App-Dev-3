using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	private int _xMove;

	public GameObject bulletObj;
	public int enemySpeed = 5;

	public bool facingLeft;

	public bool isTurret;

	// Start is called before the first frame update
	private void Start() {
		if (isTurret)
			InvokeRepeating("Shoot", 1.0f, 2.0f);
	}

	// Update is called once per frame
	private void Update() {
		if (!isTurret) {
			if (facingLeft)
				_xMove = -1;
			else
				_xMove = 1;

			var hit = Physics2D.Raycast(transform.position, new Vector2(_xMove, 0));
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_xMove, 0) * enemySpeed;

			if (hit.distance < 0.7f && !hit.collider.CompareTag("Player"))
				Flip();
		}
	}

	private void Flip() {
		GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
		facingLeft = !facingLeft;
	}

	private void Shoot() {
		gameObject.GetComponent<AudioSource>().Play();
		var bullet = Instantiate(bulletObj);
		bullet.transform.position = gameObject.transform.position;
	}
}
