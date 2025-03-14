using UnityEngine;
using UnityEngine.Pool;

public class Square : MonoBehaviour
{
    [SerializeField] GameObject outlineGO;

    public float speed = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeY();
        RandomizeSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (gameObject.transform.position.x > 15f)
        {
            //We've gone too far, time to be deleted
            //Destroy(gameObject);
            ReturnToPool();
        }
    }

    public void RandomizeY()
    {
        //Randomize the Y position between -4 and +4
        //this one-liner mostly works, but can have some weird edge case results
        //transform.position += Vector3.up * Random.Range(-4f, 4f);
        //alternatively
        transform.position = new Vector3(transform.position.x,
                                         Random.Range(-4f, 4f),
                                         transform.position.z);


    }

    void RandomizeSpeed()
    {
        //Randomize the Y position between -4 and +4
        speed = Random.Range(0.1f, 5f);

    }

    private void OnDestroy()
    {
        Debug.Log("Goodbye!");
    }

    public void ToggleOutline()
    {
        outlineGO.SetActive(!outlineGO.activeSelf);
    }

    //Infrastructure for ObjectPool
    public IObjectPool<Square> Pool;

    public void ResetSquare()
    {
        //set my position to my parent's (the spawner)
        //mostly to get us in the correct X location
        transform.position = transform.parent.position;
        outlineGO.SetActive(false);
        gameObject.GetComponent<ColorChanger>().SendMessage("RandomizeColor");
        RandomizeY();
        RandomizeSpeed();
    }

    //instead of calling Destroy we'll use this
    private void ReturnToPool()
    {
        Pool.Release(this);
    }


}
