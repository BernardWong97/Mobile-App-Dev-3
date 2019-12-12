using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Class controlling the behaviour of the player grappling hook,
 * Designed for future improvement ( grapple swing/ rope collide surfaces/ etc)
 * Adapted from https://www.raywenderlich.com/348-make-a-2d-grappling-hook-game-in-unity-part-1
 */
public class RopeController : MonoBehaviour {
	// Private variables
	private const float RopeMaxCastDistance = 3f;
	private readonly List<Vector2> _ropePositions = new List<Vector2>();
	private Vector2 _direction;
	private bool _distanceSet;
	private KeyCode _keyCode;
	private Vector2 _playerPosition;
	private Rigidbody2D _ropeHingeAnchorRb;
	private SpriteRenderer _ropeHingeAnchorSprite;
	public AudioSource grappleAudio;

	public GameObject ropeHingeAnchor;

	// Public variables
	public DistanceJoint2D ropeJoint;
	public LayerMask ropeLayerMask;
	public LineRenderer ropeRenderer;
	private bool RopeAttached { get; set; }

	private void Awake() {
		ropeJoint.enabled = false;
		_playerPosition = transform.position;
		_ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
		_ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
	}

	private void Update() {
		_playerPosition = gameObject.transform.position;
		ShootRope();
		UpdateRopePositions();
	}

	/**
	 * Determine key input and shoot rope to specific direction
	 */
	private void ShootRope() {
		if (Input.GetKeyDown(KeyCode.O)) {
			_direction = Vector2.up;
			_keyCode = KeyCode.O;
			RayCastRope();
		}
		else if (Input.GetKeyDown(KeyCode.P)) {
			_direction = Vector2.one;
			_keyCode = KeyCode.P;
			RayCastRope();
		}
		else if (Input.GetKeyDown(KeyCode.I)) {
			_direction = new Vector2(-1, 1);
			_keyCode = KeyCode.I;
			RayCastRope();
		}

		if (Input.GetKeyUp(_keyCode)) ResetRope();
	}

	/**
	 * use raycast detection to determine if it is grapple-able
	 */
	private void RayCastRope() {
		ropeRenderer.enabled = true;

		// Raycast detection
		var hit = Physics2D.Raycast(_playerPosition, _direction, RopeMaxCastDistance, ropeLayerMask);

		// if hit object with "ground" tag, grapple
		if (hit.collider != null && hit.collider.CompareTag("ground")) {
			grappleAudio.Play();
			RopeAttached = true;
			GetComponent<PlayerMovement>().canDoubleJump = false;

			if (_ropePositions.Contains(hit.point)) return; // if the rope containing the hit point, ignore

			// Jump slightly to distance the player a little from the ground after grappling to something.
			transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
			_ropePositions.Add(hit.point);
			ropeJoint.distance = Vector2.Distance(_playerPosition, hit.point);
			ropeJoint.enabled = true;
			_ropeHingeAnchorSprite.enabled = true;
		}
		else {
			ropeRenderer.enabled = false;
			RopeAttached = false;
			ropeJoint.enabled = false;
		}
	}

	/**
	 * Reset rope
	 */
	private void ResetRope() {
		ropeJoint.enabled = false;
		RopeAttached = false;
		ropeRenderer.positionCount = 2;
		ropeRenderer.SetPosition(0, transform.position);
		ropeRenderer.SetPosition(1, transform.position);
		_ropePositions.Clear();
		_ropeHingeAnchorSprite.enabled = false;
	}

	/**
	 * Update the rope coordinates
	 */
	private void UpdateRopePositions() {
		if (!RopeAttached)
			return;

		ropeRenderer.positionCount = _ropePositions.Count + 1;

		for (var i = ropeRenderer.positionCount - 1; i >= 0; i--) // for every position of the rope renderer
			if (i != ropeRenderer.positionCount - 1) {
				// if not the Last point of line renderer
				ropeRenderer.SetPosition(i, _ropePositions[i]);

				if (i == _ropePositions.Count - 1 || _ropePositions.Count == 1) {
					var ropePosition = _ropePositions[_ropePositions.Count - 1];

					if (_ropePositions.Count == 1) {
						_ropeHingeAnchorRb.transform.position = ropePosition;

						if (_distanceSet) continue;
						ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
						_distanceSet = true;
					}
					else {
						_ropeHingeAnchorRb.transform.position = ropePosition;

						if (_distanceSet) continue;
						ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
						_distanceSet = true;
					}
				}
				else if (i - 1 == _ropePositions.IndexOf(_ropePositions.Last())) {
					var ropePosition = _ropePositions.Last();
					_ropeHingeAnchorRb.transform.position = ropePosition;

					if (_distanceSet) continue;
					ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
					_distanceSet = true;
				}
			}
			else
				ropeRenderer.SetPosition(i, transform.position);
	}
}
