using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Wafe Function Collapse Terrain generator
 * 
 * Picks tiles by minimizing the "entropy" in a set of tiles.
 * "Entropy" here is just a measure of how many possible
 * tiles can legally be placed in a particular spot.
 * Each step we pick the tile with the lowest entropy, 
 * then randomly pick one of the remailing tile options.
 * Then we update the options of the neighbors, lowering
 * their entropy.
 */
public class WFC : MonoBehaviour
{

    [SerializeField] bool rerun = false;

    public int dimensions = 10;
    public Tile[] tiles;
    public List<Cell> gridComponents;
    public Cell cellPrefab;
    GameObject cellParent;
    GameObject tileParent;
    public Tile waterTile;
    public bool makeWaterBorder = false;

    int iterations;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeGrid();
    }

    void Update()
    {
        if (rerun)
        {
            InitializeGrid();
            rerun = false;
        }
    }


    public void InitializeGrid()
    {
        //set up the mapParent object
        if(cellParent != null)
        {
            //clean up the old map
            Destroy(cellParent);
            Destroy(tileParent);
        }

        cellParent = new GameObject("Cell Parent");
        tileParent = new GameObject("Tile Parent");
        iterations = 0;

        //initialize the map with our cells to track options for each tile
        gridComponents = new List<Cell>();
        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                Cell newCell = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity);
                newCell.transform.SetParent(cellParent.transform);
                newCell.SetCell(false, tiles, x, y);
                newCell.gameObject.name = newCell.ToString();
                gridComponents.Add(newCell);
            }
        }

        if (makeWaterBorder)
            SetBottomBorderToWater();

        StartCoroutine(CheckEntropy());
    }

    void SetBottomBorderToWater()
    {
        for (int x = 0; x < dimensions; x++)
        {
            //on the border
            gridComponents[x].SetTiles(new Tile[] { waterTile });
        }
    }

    IEnumerator CheckEntropy()
    {
        //make a copy of all the cells
        List<Cell> tempGrid = new List<Cell>(gridComponents);
        //remove the ones that have been collapsed
        tempGrid.RemoveAll(c => c.collapsed);
        //sort them by their entropy
        tempGrid.Sort((a, b) => { return a.Entropy() - b.Entropy(); });
        //remove all the tiles that dont have the smallest Entropy
        int smallestEntropy = tempGrid[0].tileOptions.Length;
        tempGrid.RemoveAll(x => x.tileOptions.Length != smallestEntropy);

        //add a delay so we can see it in action
        yield return new WaitForSeconds(0.01f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<Cell> lowEntropyGrid)
    {
        //select a random cell from this list
        int randIndex = UnityEngine.Random.Range(0, lowEntropyGrid.Count);
        Cell cellToCollapse = lowEntropyGrid[randIndex];
        //update the Cell in the full grid
        gridComponents[cellToCollapse.x + cellToCollapse.y * dimensions] = cellToCollapse;

        //collapse to a random option
        Tile collapsedTile = cellToCollapse.CollapseTileRandomly();

        //create the tileGO in the world
        Tile newTile = Instantiate(collapsedTile, cellToCollapse.transform.position, Quaternion.identity);
        newTile.gameObject.name = "Tile_" + cellToCollapse.x.ToString() + "_" + cellToCollapse.y.ToString();
        newTile.transform.SetParent(tileParent.transform);

        //update my neghbors list of valid options
        PropogateCollapse(cellToCollapse);

        //start the next iteration
        iterations++;
        if(iterations < dimensions * dimensions)
        {
            StartCoroutine(CheckEntropy());
        }

    }

    void PropogateCollapse(Cell collapsedCell)
    {
        //calculate the index for the collapsed cell in the grid
        int index = collapsedCell.x + collapsedCell.y * dimensions;
        int x = collapsedCell.x;
        int y = collapsedCell.y;

        //update up neighbor
        if (y < dimensions - 1)
            UpdateNeighbor(gridComponents[x + (y + 1) * dimensions], collapsedCell.tileOptions[0].upNeighbors);

        //update right neighbor
        if (x < dimensions - 1)
            UpdateNeighbor(gridComponents[(x + 1) + y * dimensions], collapsedCell.tileOptions[0].rightNeighbors);

        //update down neighbor
        if (y > 0)
            UpdateNeighbor(gridComponents[x + (y - 1) * dimensions], collapsedCell.tileOptions[0].downNeighbors);

        //update left neighbor
        if (x > 0)
            UpdateNeighbor(gridComponents[(x - 1) + y * dimensions], collapsedCell.tileOptions[0].leftNeighbors);

        
    }

    void UpdateNeighbor(Cell neighbor, Tile[] legalOptions)
    {
        List<Tile> validOptions = new List<Tile>();

        foreach(Tile tileToCheck in neighbor.tileOptions)
        {
            //is this tile in the legal list for our neighbor?
            if (Array.Exists(legalOptions, x => x == tileToCheck))
            {
                //then keep it for next time
                validOptions.Add(tileToCheck);
            }
        }
        if (validOptions.Count == 0)
            Debug.LogFormat("Neighbor: {0} has no options", neighbor);

        //update the neighbor
        neighbor.SetTiles(validOptions.ToArray());

    }

}
