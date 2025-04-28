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
        emptySpaces = new List<List<int>>();
        hulksDone = 0;
        base.GenerateMap();
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
        //pick a random empty space
        if (emptySpaces.Count == 0)
        {
            Debug.Log("No empty spaces left for Hulk to walk");
            x = mapWidth / 2;
            y = mapHeight / 2;
            return;
        }
        int index = Random.Range(0, emptySpaces.Count);
        x = emptySpaces[index][0];
        y = emptySpaces[index][1];
    }

    Vector2[] possibleWalks = new Vector2[] { new Vector2(0,1),//up
                                            new Vector2(0,-1),//down
                                            new Vector2(1,0),//right
                                            new Vector2(-1,0)};//left

    void HulkWalk(int x, int y)
    {
        Vector2 current = new Vector2(x, y);
        int type = Random.Range(1, tileset.Count);
        for (int i = 0; i < hulkenergy; i++)
        {
            Debug.Log("Hulk walking from " + current);

            Vector2 walk = possibleWalks[Random.Range(0, possibleWalks.Length)];

            current = current + walk;

            if (current.x < 0 || current.x >= mapWidth ||
                current.y < 0 || current.y >= mapHeight)
            {
                Debug.Log("Hulk walked off the edge of the map");
                break;
            }

            SetGridSpace(type, (int)current.x, (int)current.y);
            emptySpaces.Add(new List<int>() { (int)current.x, (int)current.y });
        }
        
    }

}
