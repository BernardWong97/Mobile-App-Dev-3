using UnityEngine;

/**
 * Camera controller class controlling camera behaviour.
 */
public class CameraController : MonoBehaviour {
	// Private variables
	private GameObject _player;

	// Public variables
	public float xMax;
	public float xMin;
	public float yMax;
	public float yMin;

	private void Start() {
		// Find player in scene
		_player = GameObject.FindWithTag("Player");
	}

	private void LateUpdate() {
		if (_player == null) return; // if no player, quit function

		// Get player positions
		var x = Mathf.Clamp(_player.transform.position.x, xMin, xMax);
		var y = Mathf.Clamp(_player.transform.position.y, yMin, yMax);

		// Follow the player
		transform.position = new Vector3(x, y, gameObject.transform.position.z);
	}
}
