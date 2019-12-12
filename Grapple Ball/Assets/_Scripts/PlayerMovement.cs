using System.Collections;
using UnityEngine;

/**
 * Player movement controller, handling all player movements
 */
public class PlayerMovement : MonoBehaviour {
	// Private variables
	private bool _facingLeft;
	private bool _isGrounded;
	private float _moveX;
	private Rigidbody2D _rigid2d;
	private PlayerStatus _stats;
	public Animator animator;
	public bool canDoubleJump;
	public AudioSource itemAudio;

	public AudioSource jumpAudio;

	// Public variables
	public int playerJumpForce = 1250;
	public int playerSpeed = 1;

	private void Start() {
		_rigid2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	private void Update() {
		PlayerMove();
	}

	/**
	 * Determine the movement of the player
	 */
	private void PlayerMove() {
		_moveX = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump")) {
			if (_isGrounded) {
				// Determine player on ground
				Jump();
				canDoubleJump = true;
			}
			else {
				if (canDoubleJump) {
					// Determine can double jump
					canDoubleJump = false;
					DoubleJump();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.K))
			StartCoroutine(Teleport());

		if (_moveX < 0.0f && _facingLeft == false) // Flip player facing direction
			FlipPlayer();
		else if (_moveX > 0.0f && _facingLeft)
			FlipPlayer();

		_rigid2d.velocity = new Vector2(_moveX * playerSpeed, _rigid2d.velocity.y);

		animator.SetFloat("Speed", Mathf.Abs(_moveX));
	}

	/**
	 * Make player jump
	 */
	private void Jump() {
		jumpAudio.Play();
		_rigid2d.velocity = new Vector2(_rigid2d.velocity.x, 0);
		_rigid2d.AddForce(Vector2.up * playerJumpForce);
		_isGrounded = false;
	}

	/**
	 * Make player double jump, consumes 20 gems
	 */
	private void DoubleJump() {
		_stats = gameObject.GetComponent<PlayerStatus>();

		if (_stats.gemCount < 20) return; // if gem is less than 20, ignore

		jumpAudio.Play();
		_rigid2d.velocity = new Vector2(_rigid2d.velocity.x, 0);
		_rigid2d.AddForce(Vector2.up * playerJumpForce);
		_stats.SpendDouble();
	}

	/**
	 * Teleport a player vertically upwards in a fixed distance, consumes 50 gems
	 */
	private IEnumerator Teleport() {
		_stats = gameObject.GetComponent<PlayerStatus>();

		if (_stats.gemCount < 50) yield break; // if gem is less than 50, ignore

		// Freeze the player for a second
		_rigid2d.gravityScale = 0;
		_rigid2d.velocity = Vector2.zero;

		LayerMask mask = LayerMask.GetMask("Default");

		// Raycast detection prevent teleport through objects
		var hit = Physics2D.Raycast(transform.position, Vector2.up, 3f, mask);

		if (hit.collider == null) // if nothing is in range of raycast, teleport upwards
			GetComponent<Transform>().position += transform.up * 2.5f;
		else {
			// if there is object in range of raycast, teleport under the object
			var distOffsetY = GetComponent<BoxCollider2D>().size.y / 2 - GetComponent<BoxCollider2D>().offset.y;
			GetComponent<Transform>().position = new Vector3(hit.point.x, hit.point.y - distOffsetY);
		}

		_stats.SpendTeleport();

		yield return new WaitForSeconds(1);

		jumpAudio.Play();
		_rigid2d.gravityScale = 8;
	}

	/**
	 * Flip the sprite renderer to face the other direction
	 */
	private void FlipPlayer() {
		GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
		_facingLeft = !_facingLeft;
	}

	private void OnCollisionEnter2D(Collision2D colObj) {
		if (colObj.gameObject.CompareTag("ground")) // check is on ground on collision
			_isGrounded = true;
	}
}
