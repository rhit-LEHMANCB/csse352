using UnityEngine;
using UnityEngine.Pool;

public class Square : MonoBehaviour
{
    public GameObject outlineGO;
    public float speed = 2.0f; // Any changes to this in Unity will be applied during awake

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeSpeed();
        RandomizeY();
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (gameObject.transform.position.x > 15f)
        {
            ReturnToPool();
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

    private void RandomizeY()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        transform.position = new Vector3(transform.position.x, Random.Range(-5.0f, 5.0f), transform.position.z);
    }

    private void RandomizeSpeed()
    {
        speed = Random.Range(0.1f, 5.0f);
    }

    // object pool stuff

    public IObjectPool<Square> Pool;

    public void ResetSquare()
    {
        // all the stuff to start off a square when it is reused
        transform.position = transform.parent.position;
        RandomizeY();
        RandomizeSpeed();
        outlineGO.SetActive(false);
        gameObject.GetComponent<ColorChanger>().SendMessage("RandomizeColor");
    }

    public void ReturnToPool()
    {
        // what to do when im done being used
        Pool.Release(this);
    }
}
