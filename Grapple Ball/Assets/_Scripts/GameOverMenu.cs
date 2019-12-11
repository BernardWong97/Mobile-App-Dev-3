using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {
	public GameObject menuUI;

	public void Retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void QuitGame() {
		Debug.Log("Quit");
		Application.Quit();
	}

	public void NavToMenu() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}
