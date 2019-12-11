using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
	public GameObject loadingScreen;
	public Text progressText;
	public Slider slider;

	public void LoadLevel(int sceneIndex) {
		StartCoroutine(LoadAsync(sceneIndex));
	}

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
