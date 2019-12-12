using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Pause menu controller
 */
public class PauseMenu : MonoBehaviour {
	// Private variables
	private static bool IsPaused;

	// Public variables
	public GameObject menuUI;
	public GameObject settingsUI;

	// Update is called once per frame
	private void Update() {
		if (!Input.GetKeyDown(KeyCode.Escape)) return; // if keypress is not esc, ignore

		if (IsPaused)
			Resume();
		else
			Pause();
	}

	/**
	 * Resume the game
	 */
	public void Resume() {
		if (settingsUI.activeSelf) {
			// if in setting menu, return to pause menu
			settingsUI.SetActive(false);
			Pause();
		}
		else {
			menuUI.SetActive(false);
			Time.timeScale = 1f;
			IsPaused = false;
		}
	}

	/**
	 * Pause the game
	 */
	private void Pause() {
		menuUI.SetActive(true);
		Time.timeScale = 0f;
		IsPaused = true;
	}

	/**
	 * Quit the game
	 */
	public void QuitGame() {
		Debug.Log("Quit");
		Application.Quit();
	}

	/**
	 * Navigate to main menu and delete player data to reset game
	 */
	public void NavToMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
		DataManagement.DeleteData();
	}
}
