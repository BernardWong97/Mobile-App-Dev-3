using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
	public GameObject eraseCanvas;
	public GameObject eraseOverlay;
	public LevelLoader levelLoader;

	private void OnTriggerEnter2D(Collider2D colObj) {
		if (!colObj.CompareTag("Player")) return;
		colObj.gameObject.GetComponent<PlayerStatus>().SavePlayer();
		eraseCanvas.SetActive(false);
		eraseOverlay.SetActive(false);
		levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
		Debug.Log("Level Complete");
	}
}
