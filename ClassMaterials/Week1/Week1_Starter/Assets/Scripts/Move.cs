using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject outlineGO;
    public float speed = 2.0f; // Any changes to this in Unity will be applied during awake

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = Random.Range(0.1f, 5.0f);
        Transform transform = gameObject.GetComponent<Transform>();
        transform.position = new Vector3(transform.position.x, Random.Range(-5.0f, 5.0f), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (gameObject.transform.position.x > 15f)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Goodbye!");
    }

    public void ToggleOutline()
    {
        outlineGO.SetActive(!outlineGO.activeSelf);
    }
}
