using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Complete level controller
 */
public class LevelEnd : MonoBehaviour {
	public GameObject camera;

	public GameObject compelteUI;

	// Public variables
	public GameObject eraseCanvas;
	public GameObject eraseOverlay;
	public AudioSource levelCompleteAudio;
	public LevelLoader levelLoader;

	private void OnTriggerEnter2D(Collider2D colObj) {
		if (!colObj.CompareTag("Player")) return; // if collision object is not player, ignore
		colObj.gameObject.GetComponent<PlayerStatus>().SavePlayer(); // save player data
		StartCoroutine(ShowUI());
		Invoke("ChangeScene", 3); // change scene after 3 seconds (after music played)
	}

	/**
	 * Show complete level UI and disable canvases and overlays
	 */
	private IEnumerator ShowUI() {
		compelteUI.SetActive(true);
		eraseCanvas.SetActive(false);
		eraseOverlay.SetActive(false);
		camera.GetComponent<AudioSource>().Stop();
		levelCompleteAudio.Play();
		yield return new WaitForSeconds(3);
	}

	/**
	 * Change scene to next level
	 */
	private void ChangeScene() {
		levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
