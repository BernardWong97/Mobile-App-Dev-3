using UnityEngine;
using UnityEngine.Audio;

/**
 * Settings menu controller
 */
public class SettingsMenu : MonoBehaviour {
	public AudioMixer audioMixer;

	/**
	 * Set the volume of the game
	 */
	public void SetVolume(float volume) {
		audioMixer.SetFloat("gameVolume", volume);
	}

	/**
	 * Set the volume of the music
	 */
	public void SetMusic(float volume) {
		audioMixer.SetFloat("musicVolume", volume);
	}
}
