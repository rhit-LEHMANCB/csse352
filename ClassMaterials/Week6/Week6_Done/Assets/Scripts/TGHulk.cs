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
        //set up the first empty space
        emptySpaces = new List<List<int>>();
        for(int x = 0; x < mapWidth; x++) {
            emptySpaces.Add(new List<int>());
        }

        int midX = mapWidth / 2;
        int midY = mapHeight / 2;
        SetGridSpace(1, midX, midY);

        hulksDone = 0;


        base.GenerateMap();
    }

    protected override void SetGridSpace(int tileType, int x, int y)
    {
        if (!emptySpaces[x].Contains(y)) {
            emptySpaces[x].Add(y);
        }

        idGrid[x, y] = tileType;
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

    void SelectStartingPosition(out int x,out int y)
    {
        List<int> validX = new List<int>();
        for (int i = 0; i < mapWidth; i++) {
            if (emptySpaces[i].Count > 0)
                validX.Add(i);
            }
        x = RandomChoose(validX);
        y = RandomChoose(emptySpaces[x]);
    }


    Vector2[] possibleWalks = new Vector2[] { new Vector2(0,1),//up
                                            new Vector2(0,-1),//down
                                            new Vector2(1,0),//right
                                            new Vector2(-1,0)};//left

    void HulkWalk(int x, int y)
    {
        int tileType = Random.Range(1, tileset.Count);//1+ so its not a wall

        for (int i = 0; i < hulkenergy; i++)
        {
            Vector2 walkDir = possibleWalks[Random.Range(0, possibleWalks.Length)];
            x += (int)walkDir.x;
            y += (int)walkDir.y;


            if (x < 0 || x >= mapWidth ||
                y < 0 || y > mapHeight)
            {
                //walked off the edge
                break;
            }
            SetGridSpace(tileType, x, y);
        }
    }

}
