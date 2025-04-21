using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //all these are protected so that the inheriting class can access them

    //collections of the tiles
    protected Dictionary<int, GameObject> tileset;
    protected Dictionary<int, GameObject> tileGroups;

    //the individual tile types
    [SerializeField] protected GameObject prefabPlains;
    [SerializeField] protected GameObject prefabGrass;
    [SerializeField] protected GameObject prefabHills;
    [SerializeField] protected GameObject prefabMountain;

    //map size
    [SerializeField] protected int mapWidth = 50;
    [SerializeField] protected int mapHeight = 50;

    //The actual tiles
    protected List<List<GameObject>> tileGrid;
    //the ID number (e.g. tile type) of each tile
    //the ID number refers to the index in the `tileset' Dictionary
    protected int[,] idGrid;

    // Start is called before the first frame update
    void Start()
    {
        CreateTileset();
        CreateTileGroups();
        Regenerate();
    }

    /*
     * Populates the tileset dictionary with all of the prefab tiles.
     * The choice of integer id (key) in these dictionaries does matter
     * for some of the algorithms.
     */
    protected void CreateTileset()
    {

        tileset = new Dictionary<int, GameObject>();
        tileset.Add(3, prefabPlains);
        tileset.Add(1, prefabGrass);
        tileset.Add(2, prefabHills);
        tileset.Add(0, prefabMountain);
    }

    /*
     * Creates a single game object in the hierarchy for each type of tile.
     * The tiles of that type are stored as children of this game object.
     * This is not needed for any of the algorithms, it is just for organizational purposes.
     */
    protected void CreateTileGroups()
    {
        tileGroups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefabPair in tileset)
        {

            GameObject tileGroup = new GameObject(prefabPair.Value.name);
            tileGroup.transform.SetParent(gameObject.transform);
            tileGroups.Add(prefabPair.Key, tileGroup);
        }
    }

    /*
     * Creates tiles across the entire map based on the integer ids
     * stored in the `idGrid' field.
     */
    protected void MakeTiles()
    {
        //delete the old tiles
        ClearMap();
        for (int x = 0; x < mapWidth; x++)
        {
            tileGrid.Add(new List<GameObject>());
            for (int y = 0; y < mapHeight; y++)
            {
                CreateTile(idGrid[x, y], x, y);

            }
        }

    }

    /*
     * Creates a tile at a specific set of coordinates.
     * Sets the tile GameObject parent to the appropriate tilegroup object.
     */
    protected void CreateTile(int tileID, int x, int y)
    {
        GameObject tilePrefab = tileset[tileID];
        GameObject tileGroup = tileGroups[tileID];
        GameObject tile = Instantiate(tilePrefab, tileGroup.transform);
        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        //localPosition matters here, because I'm setting it relative to the "map" parent GO in the hierarchy
        tile.transform.localPosition = new Vector3(x, y, 0);
        tileGrid[x].Add(tile);
    }


    /*
     * When called the map is regenerated completely.
     */
    protected virtual void Regenerate()
    {
        initializeGrid();
        //make new groups
        ClearMap();
        tileGrid = new List<List<GameObject>>();
        GenerateMap();
    }


    //used in GenerateMap to protect us from infinite loops while debugging
    protected int failSafeCounter = 0;

    /*
     * Implements the algorithm to create the map, should be overridden
     * by subclasses. This default simply generates a uniform map of one tile.
     */
    protected virtual void GenerateMap()
    {
        bool done = false;
        failSafeCounter = 0;

        while (!done && failSafeCounter < mapHeight*mapWidth*2)
        {
            //check if we want to wait and draw the steps one at a time
            if (slowGen)
            {
                StartCoroutine(StepDelayCoroutine());
                done = true;//we're passing control to the Coroutine entirely for this
            }
            else //if not jsut do it all at once and draw the final result
            {
                done = Step();
            }
            failSafeCounter++;
        }
        MakeTiles();
    }

    /*
     * Helper function to delete old tiles to stop memory leaks.
     */
    protected void ClearMap()
    {
        //delete the old groups of tiles first
        foreach (GameObject group in tileGroups.Values)
        {
            Destroy(group);
        }
        //make new groups
        CreateTileGroups();

    }

    /*
     * Helper function that initializes the map to one type of tile.
     */
    protected void initializeGrid()
    {
        idGrid = new int[mapWidth, mapHeight];
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                idGrid[x, y] = 0;

            }
        }
    }

    /*
     * Implements one `step' of the generation algorithm. 
     * Should be overridden by subclasses.
     * return true if the algorithm is done, false otherwise.
     */
    protected virtual bool Step()
    {
        //do nothing here
        return true;
    }

    /*
     * Choose a random item from a list of items.
     * Just a little helper, we do this a lot in this code.
     */
    protected T RandomChoose<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    /*
     * Sets the tile type in the idGrid. 
     * Some subclasses may override this behavior.
     * 
     */
    protected virtual void SetGridSpace(int tileType, int x, int y)
    {
        idGrid[x, y] = tileType;
    }



    //=============
    //just for debugging
    [SerializeField] bool rerun = false;

    /*
     * This update simply re-runs the generation if the `rerun' parameter
     * is set to true in the editor at run-time.
     */
    private void Update()
    {
        if (rerun)
        {
            Regenerate();
            rerun = false;
        }
    }

    //if true this delays the generation to draw one `Step' at a time so we can watch the
    //generation occur
    [SerializeField] protected bool slowGen = true;
    //the amount of time to wait for one step of generation before starting the next
    [SerializeField] float timer = 1f;
    //state variable that tells us if we are waiting for one Step to finish
    protected bool stepRunning = false;
    protected IEnumerator StepDelayCoroutine()
    {
        stepRunning = true;
        bool isDone = Step();//take one step
        MakeTiles();//create the tiles
        yield return new WaitForSeconds(timer);
        failSafeCounter++;
        //this is bait for infinite loops, be careful
        if (!isDone & failSafeCounter < mapHeight*mapWidth*2)
            StartCoroutine(StepDelayCoroutine());
    }

    //=============
}
