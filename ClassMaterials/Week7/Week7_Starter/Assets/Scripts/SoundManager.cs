using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainAudioSource;
    public float volume = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShot(AudioClip clip)
    {
        mainAudioSource.PlayOneShot(clip);
    }

    private void OnGUI()
    {
        // Create a slider for volume control
        volume = GUI.HorizontalSlider(new Rect(10, 10, 100, 30), volume, 0.0f, 1.0f);
        mainAudioSource.volume = volume;
        // Create a button to play the sound
        if (GUI.Button(new Rect(10, 50, 100, 30), "Mute"))
        {
            mainAudioSource.mute = !mainAudioSource.mute;
        }
    }
}
