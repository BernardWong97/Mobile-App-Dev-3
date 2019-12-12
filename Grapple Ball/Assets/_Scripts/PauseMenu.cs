using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public static bool IsPaused;

	public GameObject menuUI;

	public GameObject settingsUI;

	// Update is called once per frame
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (IsPaused)
				Resume();
			else
				Pause();
		}
	}

	public void Resume() {
		if (settingsUI.activeSelf) {
			settingsUI.SetActive(false);
			Pause();
		}
		else {
			menuUI.SetActive(false);
			Time.timeScale = 1f;
			IsPaused = false;
		}
	}

	private void Pause() {
		menuUI.SetActive(true);
		Time.timeScale = 0f;
		IsPaused = true;
	}

	public void QuitGame() {
		Debug.Log("Quit");
		Application.Quit();
	}

	public void NavToMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
		DataManagement.DeleteData();
	}
}
