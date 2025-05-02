using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{
    SoundManager soundManager;
    AudioSource audioSource;

    public bool localSounds = false;
    public AudioClip spawnSound;
    public AudioClip crashSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(BurnOut());
    }

    public float speed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public float burnTime = 5f;
    IEnumerator BurnOut()
    {
        //play a sound here when I first spawn
        PlaySound(spawnSound);
        float timeLeft = burnTime;
        while(timeLeft > 0)
        {
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        //Destroy(gameObject); //change this to make the audio work
        this.Destroy();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //I hit something
        Debug.LogFormat("I hit {0}", collider.gameObject.name);
        //Destroy(gameObject); //change this to make the audio work
        this.Destroy();
    }

    public void Destroy()
    {
        //hite the visuals
        GetComponent<Renderer>().enabled = false;
        //turn off our speed so we dont keep moving
        speed = 0f;

        StartCoroutine(CustomDestroy());
    }

    private IEnumerator CustomDestroy()
    {
        //play a sound here when i time out or hit something
        PlaySound(crashSound);
        yield return new WaitForSeconds(crashSound.length);
        Destroy(gameObject);
    }

    //just to show off the difference
    void PlaySound(AudioClip clip)
    {
        if (localSounds)
            audioSource.PlayOneShot(clip);
        else
            soundManager.PlayOneShot(clip);
    }
}
