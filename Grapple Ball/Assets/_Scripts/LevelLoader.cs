using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Controller handling loading level UI
 */
public class LevelLoader : MonoBehaviour {
	// Public variables
	public GameObject loadingScreen;
	public Text progressText;
	public Slider slider;

	/**
	 * Execute coroutine on load async function
	 */
	public void LoadLevel(int sceneIndex) {
		StartCoroutine(LoadAsync(sceneIndex));
	}

	/**
	 * Load sceneIndex scene while display loading progress asynchronously
	 */
	private IEnumerator LoadAsync(int sceneIndex) {
		var asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

		loadingScreen.SetActive(true);

		while (!asyncOperation.isDone) {
			var progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
			slider.value = progress;
			progressText.text = progress * 100f + "%";

			yield return null;
		}
	}
}
