using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {
	public AudioMixer audioMixer;

	public void SetVolume(float volume) {
		audioMixer.SetFloat("gameVolume", volume);
	}

	public void SetMusic(float volume) {
		audioMixer.SetFloat("musicVolume", volume);
	}
}
