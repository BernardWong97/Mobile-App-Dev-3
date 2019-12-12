using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
	public GameObject eraseCanvas;
	public GameObject eraseOverlay;
	public GameObject compelteUI;
	public GameObject camera;
	public LevelLoader levelLoader;
	public AudioSource levelCompleteAudio;
	
	
	private void OnTriggerEnter2D(Collider2D colObj) {
		if (!colObj.CompareTag("Player")) return;
		colObj.gameObject.GetComponent<PlayerStatus>().SavePlayer();
		StartCoroutine(ShowUI());
		Invoke("ChangeScene", 3);
	}

	IEnumerator ShowUI() {
		compelteUI.SetActive(true);
		eraseCanvas.SetActive(false);
		eraseOverlay.SetActive(false);
		camera.GetComponent<AudioSource>().Stop();
		levelCompleteAudio.Play();
		yield return new WaitForSeconds(3);
	}

	void ChangeScene() {
		levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
