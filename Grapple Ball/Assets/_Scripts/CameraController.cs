using UnityEngine;

public class CameraController : MonoBehaviour {
	private GameObject player;
	public float xMax;

	public float xMin;
	public float yMax;
	public float yMin;

	// Start is called before the first frame update
	private void Start() {
		player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	private void LateUpdate() {
		if (player != null) {
			var x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
			var y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

			gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
		}
	}
}
