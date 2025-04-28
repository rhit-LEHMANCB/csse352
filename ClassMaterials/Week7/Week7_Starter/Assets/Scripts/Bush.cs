using UnityEngine;

public class Bush : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float r = Random.Range(0.5f, 3f);
        transform.localScale = new Vector3(r, r, r);

        gameObject.GetComponent<Renderer>().material.color = Color.green;

        transform.position = new Vector3(
            Random.Range(-25, 25),
            transform.position.y,
            Random.Range(-25, 25));
    }
}
