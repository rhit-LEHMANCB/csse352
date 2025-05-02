using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainAudioSource;
    public float volume = 1f;



    public void PlayOneShot(AudioClip clip)
    {
        mainAudioSource.PlayOneShot(clip);
    }

    public void AdjustVolume(float vol)
    {
        mainAudioSource.volume += vol;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Volume -"))
            AdjustVolume(-0.1f);

        if (GUILayout.Button("Volume +"))
            AdjustVolume(0.1f);

        if (GUILayout.Button("Mute"))
            mainAudioSource.mute = !mainAudioSource.mute;
    }

}
