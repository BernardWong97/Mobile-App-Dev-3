using UnityEngine;

/**
 * Script handling menu ball's rope
 */
public class MenuRope : MonoBehaviour {
	// Public variables
	public GameObject ball;
	public LineRenderer lineRenderer;
	public GameObject plane;

	private void Update() {
		CreateRope();
	}

	/**
	 * Create and render a rope from ball to plane
	 */
	private void CreateRope() {
		var planePosition = plane.transform.position;
		var ballPosition = ball.transform.position;

		lineRenderer.enabled = true;
		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition(0, ballPosition);
		lineRenderer.SetPosition(1, planePosition);
	}
}
