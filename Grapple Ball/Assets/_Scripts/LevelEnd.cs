using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
	public GameObject eraseCanvas;
	public GameObject eraseOverlay;
	public LevelLoader levelLoader;

	private void OnTriggerEnter2D(Collider2D colObj) {
		eraseCanvas.SetActive(false);
		eraseOverlay.SetActive(false);
		levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
		Debug.Log("Tutorial Complete");
	}
}
