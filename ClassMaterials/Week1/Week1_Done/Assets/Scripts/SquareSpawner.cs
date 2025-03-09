using UnityEngine;

public class SquareSpawner : MonoBehaviour
{

    [SerializeField] GameObject squarePrefab;
    [SerializeField] float spawnTime = 0.0f;
    [SerializeField] float spawnCooldown = 0.5f;

    [SerializeField] bool SetRed = false;

    private int _numSpawned = 0;
    Move fastestSquare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnSquare();
        SpawnSquare();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnInUpdate();
    }

    void SpawnInUpdate()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime < 0)
        {
            SpawnSquare();
            spawnTime = spawnCooldown;
        }

        if (_numSpawned == 5)
        {
            foreach(Transform child in transform)
            {
                //this will occasionally cause NullReferences
                child.SendMessage("SetColor", Color.red);
            }
        }

    }

    public void SpawnSquare()
    {
        //make a new square by copying our prefab
        //put it under the spawner in the hierarchy
        //and also set the starting position to the same as the spawner
        GameObject newSquare = Instantiate(squarePrefab, gameObject.transform);
        Move m = newSquare.GetComponent<Move>();
        _numSpawned += 1;

        if (fastestSquare == null || fastestSquare.speed < m.speed)
        {
            //turn off the outline on the old square
            if (fastestSquare != null)
                fastestSquare.ToggleOutline();
            fastestSquare = m;
            //turn on the outline on this square
            fastestSquare.ToggleOutline();
        }

    }

    public void SlowDown()
    {
        if (fastestSquare == null)
            return;
        //slow down the fastest square by 50%
        fastestSquare.speed *= 0.5f;
    }

}
