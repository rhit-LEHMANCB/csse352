using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Hulk Terrain Generator
 * 
 * hulk is dropped into a room
 * he wanders around randomly destroying walls
 * until he is tired or walks off the edge of the map
 */
public class TGHulk : TerrainGenerator
{
    [SerializeField] int numHulks = 15;
    [SerializeField] int hulkenergy = 25;
    int hulksDone;

    List<List<int>> emptySpaces;
    protected override void GenerateMap()
    {
       
    }

    protected override void SetGridSpace(int tileType, int x, int y)
    {
       
    }


    //one hulk doing its thing
    protected override bool Step()
    {
        int x, y;
        SelectStartingPosition(out x, out y);

        HulkWalk(x, y);

        hulksDone++;

        return hulksDone >= numHulks;

    }

    void SelectStartingPosition(out int x, out int y)
    {
        x = 0;
        y = 0; 
    }

    Vector2[] possibleWalks = new Vector2[] { new Vector2(0,1),//up
                                            new Vector2(0,-1),//down
                                            new Vector2(1,0),//right
                                            new Vector2(-1,0)};//left

    void HulkWalk(int x, int y)
    {
       
    }

}
