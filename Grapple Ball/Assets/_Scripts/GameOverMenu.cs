using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Game over menu controller
 */
public class GameOverMenu : MonoBehaviour {
	public GameObject menuUI;

	/**
	 * Reload current scene
	 */
	public void Retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/**
	 * Quit game
	 */
	public void QuitGame() {
		Debug.Log("Quit");
		Application.Quit();
	}

	/**
	 * Navigate to main menu
	 */
	public void NavToMenu() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}
