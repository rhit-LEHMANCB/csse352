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
        
        //add a delay so we can see it in action
        yield return new WaitForSeconds(0.01f);

    }

    void CollapseCell(List<Cell> lowEntropyGrid)
    {
        int randIndex = UnityEngine.Random.Range(0, lowEntropyGrid.Count);
        Cell collapsedCell = lowEntropyGrid[randIndex];

        // update the cell in the grid
        gridComponents[collapsedCell.x + collapsedCell.y * dimensions] = collapsedCell;

        Tile collapsedTile = collapsedCell.CollapseTileRandomly();

        Tile newTile = Instantiate(collapsedTile, collapsedCell.transform.position, Quaternion.identity);

        newTile.transform.SetParent(tileParent.transform);

        PropogateCollapse(collapsedCell);

        iterations++;
        if (iterations < dimensions * dimensions)
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

        foreach (Tile tileToCheck in neighbor.tileOptions)
        {
            if (Array.Exists(legalOptions, element => element == tileToCheck))
            {
                validOptions.Add(tileToCheck);
            }
        }

        if (validOptions.Count == 0)
        {
            Debug.LogFormat("Error: No valid options for neighbor cell {0}", neighbor);
            return;
        }

        neighbor.SetTiles(validOptions.ToArray());
    }

}
