using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] GameObject squarePrefab;

    [SerializeField] float spawnTime = 0.0f;
    [SerializeField] float spawnCooldown = 0.5f;

    Move fastestSquare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        GameObject newSquare = Instantiate(squarePrefab, gameObject.transform);

        Move m = newSquare.GetComponent<Move>();
        
        if (fastestSquare == null || fastestSquare.speed < m.speed)
        {
            if (fastestSquare != null) 
                fastestSquare.ToggleOutline(); // turn off old outline
            fastestSquare = m;
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
