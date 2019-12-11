using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public Animator animator;

	public bool canDoubleJump;

	private bool facingLeft;

	private bool isGrounded;

	public AudioSource itemAudio;

	public AudioSource jumpAudio;

	private float moveX;

	public int playerJumpForce = 1250;
	public int playerSpeed = 1;
	private Rigidbody2D rigid2d;

	private PlayerStatus stats;

	private void Start() {
		rigid2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	private void Update() {
		PlayerMove();
	}

	private void PlayerMove() {
		moveX = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump")) {
			if (isGrounded) {
				Jump();
				canDoubleJump = true;
			}
			else {
				if (canDoubleJump) {
					canDoubleJump = false;
					DoubleJump();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.K))
			StartCoroutine(Teleport());

		if (moveX < 0.0f && facingLeft == false)
			FlipPlayer();
		else if (moveX > 0.0f && facingLeft)
			FlipPlayer();

		rigid2d.velocity = new Vector2(moveX * playerSpeed, rigid2d.velocity.y);

		animator.SetFloat("Speed", Mathf.Abs(moveX));
	}

	private void Jump() {
		jumpAudio.Play();
		rigid2d.velocity = new Vector2(rigid2d.velocity.x, 0);
		rigid2d.AddForce(Vector2.up * playerJumpForce);
		isGrounded = false;
	}

	private void DoubleJump() {
		stats = gameObject.GetComponent<PlayerStatus>();

		if (stats.gemCount >= 20) {
			jumpAudio.Play();
			rigid2d.velocity = new Vector2(rigid2d.velocity.x, 0);
			rigid2d.AddForce(Vector2.up * playerJumpForce);
			stats.SpendDouble();
		}
	}

	private IEnumerator Teleport() {
		stats = gameObject.GetComponent<PlayerStatus>();

		if (stats.gemCount >= 50) {

			rigid2d.gravityScale = 0;
			rigid2d.velocity = Vector2.zero;
			LayerMask mask = LayerMask.GetMask("Default");
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 3f, mask);

			if (hit.collider == null) {
				GetComponent<Transform>().position += transform.up * 2.5f;
			}
			else {
				float distOffsetY = GetComponent<BoxCollider2D>().size.y / 2 - GetComponent<BoxCollider2D>().offset.y;
					GetComponent<Transform>().position = new Vector3(hit.point.x, hit.point.y - distOffsetY);
			}


			stats.SpendTeleport();
			
			yield return new WaitForSeconds(1);
			
			jumpAudio.Play();
			rigid2d.gravityScale = 8;
		}
	}

	private void FlipPlayer() {
		facingLeft = !facingLeft;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	private void OnCollisionEnter2D(Collision2D colObj) {
		if (colObj.gameObject.CompareTag("ground"))
			isGrounded = true;
	}
}
