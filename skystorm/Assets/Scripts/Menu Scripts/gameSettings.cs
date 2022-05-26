using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameSettings : MonoBehaviour {

    public GameObject homeMenu;
    public GameObject optionsMenu;
    public Slider AudioSlider;

    private bool optionsOpen = false;
    private AudioListener AudioListener;

	void Start () {
        AudioSlider.value = AudioListener.volume;
    }
	
	void Update () {
	
	}

    public void ToggleOptions()
    {
        optionsOpen = !optionsOpen;
        optionsMenu.SetActive(optionsOpen);
        homeMenu.SetActive(!optionsOpen);
    }

    public void AudioVolume(float newValue)
    {
        AudioListener.volume = newValue;
        Debug.Log(AudioListener.volume);
    }
}
