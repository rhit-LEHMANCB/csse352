using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Diffusion Limited Aggregation Terrain Generator
 * 
 * Selects a random spot on the map
 * then fires a "particle" in a random cardinal direction.
 * When/if it hits an open tile it carves out the last 
 * "occupied" tile it came from.
 */

public class TGDLA : TerrainGenerator
{
    [SerializeField] int numParticles = 500;
    [SerializeField] bool randomParticle = false;
    [SerializeField] float changeProb = 0.1f;
    private int type = 1;

    int particlesFired;

    protected override void GenerateMap()
    {
        //initially the space in the middle is open
        //could make this a random location if you
        //dont want the map always "centered"
        int midX = mapWidth / 2;
        int midY = mapHeight / 2;
        idGrid[midX, midY] = 1;//1 here is just aribtrary, anything other than 0 is fine

        //reset our counter
        particlesFired = 0;

        base.GenerateMap();
    }

    Vector2[] possibleDirs = new Vector2[] { Vector2.up,//up
                                            Vector2.down,//down
                                            Vector2.right,//right
                                            Vector2.left };//left
    protected override bool Step()
    {
        int x = Random.Range(0, mapWidth);
        int y = Random.Range(0, mapHeight);
        Vector2 dirFire = possibleDirs[Random.Range(0, possibleDirs.Length)];
        Vector2 dirTest = Vector2.up;
        if (dirFire == Vector2.up)
        {
            dirTest = Random.Range(0.0f, 1.0f) < 0.5f ? Vector2.left : Vector2.right;
        }
        else if (dirFire == Vector2.down)
        {
            dirFire = Random.Range(0.0f, 1.0f) < 0.5f ? Vector2.left : Vector2.right;
        }
        else if (dirFire == Vector2.right)
        {
            dirFire = Random.Range(0.0f, 1.0f) < 0.5f ? Vector2.up : Vector2.down;
        }
        else if (dirFire == Vector2.left)
        {
            dirFire = Random.Range(0.0f, 1.0f) < 0.5f ? Vector2.up : Vector2.down;
        }
        for (int i = 0; i < Mathf.Max(mapWidth, mapHeight); i++)
        {
            //Debug.Log(" " + x + " " + y + " " + dirFire.x + " " + dirFire.y);
            if (CheckHit(ref x, ref y, dirFire) != -1)
            {
                if (randomParticle && Random.Range(0.0f, 1.0f) < changeProb)
                {
                    type = Random.Range(0, tileset.Count);
                }
                Debug.Log("Hit: " + x + " " + y + " " + dirFire.x + " " + dirFire.y);
                SetGridSpace(type, x, y);
                particlesFired++;
                return particlesFired >= numParticles;
            }
            x += (int)dirTest.x;
            y += (int)dirTest.y;
            if (x >= mapWidth && dirTest.x == 1)
            {
                x = 0;
            }
            else if (x < 0 && dirTest.x == -1)
            {
                x = mapWidth - 1;
            }
            else if (y >= mapHeight && dirTest.y == 1)
            {
                y = 0;
            }
            else if (y < 0 && dirTest.y == -1)
            {
                y = mapHeight - 1;
            }
        }
        //Debug.Log("Missed: " + x + " " + y + " " + dirFire.x + " " + dirFire.y + " " + particlesFired);
        return particlesFired >= numParticles; 
    }

    /*
     * Follows a direction from a starting x and y
     * until we reach the edge of the map.
     * If we hit an `open' space (idGrid != 0)
     * then we return the type of that space
     * and modify the x and y values to be the 
     * location of the last `closed' (idGrid=0)
     * space we traveled through.
     * Otherwise we return -1.
     */
    private int CheckHit(ref int x, ref int y, Vector2 dirFire)
    {
        while (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
        {
            if (idGrid[x, y] != 0)
            {
                x -= (int)dirFire.x;
                y -= (int)dirFire.y;
                return idGrid[x, y];
            }
            x += (int)dirFire.x;
            y += (int)dirFire.y;
        }
        return -1;
    }
}
