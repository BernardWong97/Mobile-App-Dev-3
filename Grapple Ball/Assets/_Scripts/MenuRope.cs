using UnityEngine;

public class MenuRope : MonoBehaviour {
	public GameObject ball;

	public LineRenderer lineRenderer;
	public GameObject plane;

	private void Update() {
		CreateRope();
	}

	private void CreateRope() {
		var planePosition = plane.transform.position;
		var ballPosition = ball.transform.position;

		lineRenderer.enabled = true;
		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition(0, ballPosition);
		lineRenderer.SetPosition(1, planePosition);
	}
}
