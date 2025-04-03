using UnityEngine;

public class PooledSquareSpawner : MonoBehaviour
{
    [SerializeField] float spawnTime = 0.0f;
    [SerializeField] float spawnCooldown = 0.5f;

    Square fastestSquare;

    SquareObjectPool pool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = gameObject.GetComponent<SquareObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnInUpdate();
    }

    void SpawnInUpdate()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            SpawnSquare();
            spawnTime = spawnCooldown;
        }
    }

    int _numSpawned = 0;
    public void SpawnSquare()
    {
        //GameObject newSquare = Instantiate(squarePrefab, gameObject.transform);

        Square newSquare = pool.Pool.Get();

        if (fastestSquare == null || fastestSquare.speed < newSquare.speed)
        {
            if (fastestSquare != null)
                fastestSquare.ToggleOutline(); // turn off old outline
            fastestSquare = newSquare;
            fastestSquare.ToggleOutline(); // turn on new outline
        }

        _numSpawned++;

        if (_numSpawned == 5)
        {
            foreach (Transform child in transform)
            {
                child.SendMessage("SetColor", Color.red);
            }
        }
    }

    public void SlowDown()
    {
        if (fastestSquare == null)
        {
            return;
        }

        fastestSquare.speed *= 0.5f;
    }
}
