using UnityEngine;
using System.Collections;

public class audioFading : MonoBehaviour {

    private AudioSource Audio;
    public float fadeSpeed = 0.008f;

    private int fadeDir = 1;
    private float fadeMin;
    void Start()
    {
        Audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(fadeDir == 1 && Audio.volume != 0.125f)
        {
            Audio.volume += fadeDir * fadeSpeed * Time.unscaledDeltaTime;
            Audio.volume = Mathf.Clamp(Audio.volume, fadeMin, 0.125f);
        }
        if(fadeDir == -1 && Audio.volume != fadeMin)
        {
            Audio.volume += fadeDir * fadeSpeed * Time.unscaledDeltaTime;
            Audio.volume = Mathf.Clamp(Audio.volume, fadeMin, 0.125f);
        }
    }

    public void fadeMusic(int direction, float min)
    {
        fadeDir = direction;
        fadeMin = min;
    }
}
