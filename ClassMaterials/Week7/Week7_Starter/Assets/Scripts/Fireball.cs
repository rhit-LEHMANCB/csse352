using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        yield return new WaitForSeconds(burnTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
