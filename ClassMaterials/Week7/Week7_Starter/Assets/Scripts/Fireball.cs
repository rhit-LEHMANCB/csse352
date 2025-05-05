using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public AudioClip spawnSound;
    public AudioClip crashSound;
    public SoundManager soundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
        GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(BurnOut());
    }

    public float speed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public float burnTime = 5f;

    IEnumerator BurnOut()
    {
        soundManager.PlayOneShot(spawnSound);

        yield return new WaitForSeconds(burnTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        soundManager.PlayOneShot(crashSound);
    }
}
