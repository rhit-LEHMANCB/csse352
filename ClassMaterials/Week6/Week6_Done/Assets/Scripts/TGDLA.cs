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

    protected override bool Step()
    {
        int x, y, startX, startY;
        //pick a random starting spot
        startX = Random.Range(0, mapWidth);
        startY = Random.Range(0, mapHeight);

        //pick a direction to fire from the starting spot
        Vector2 dirFire, dirTest;
        switch (Random.Range(0, 4))
        {
            case 0://fire down
                dirFire = new Vector2(0, -1);//the direction we are firing
                dirTest = new Vector2(1, 0);//the direction we walk along the map and try to fire after a miss
                break;
            case 1://fire left
                dirFire = new Vector2(-1, 0);
                dirTest = new Vector2(0, 1);
                break;
            case 2://fire right
                dirFire = new Vector2(0, 1);
                dirTest = new Vector2(1, 0);
                break;
            default://fire up
                dirFire = new Vector2(1, 0);
                dirTest = new Vector2(0, 1);
                break;
        }

        //a 50% chance to walk the `other way' from our starting position
        //this keeps the particles from being biased towards one quadrant of the map.
        if (Random.value < 0.5)
            dirTest *= -1;

        //try and fire from the starting spot
        //if it was a miss then move one tile over (based on dirTest)
        //and try again
        //if we have tried every single row/column then we give up and choose a new starting spot
        int tries = 0;
        while (tries < Mathf.Max(mapHeight, mapWidth))
        {
            tries++;
            x = startX;
            y = startY;
            //return the type of the tile hit, or -1 if no hit
            int hit = CheckHit(ref x, ref y, dirFire);

            //if hit and it wasnt an edge
            if (hit != -1 &&
                x != 0 && x != mapWidth - 1 &&
                y != 0 && y != mapHeight - 1)
            {
                int tileType;
                if (!randomParticle)
                {
                    //just always use type 1
                    tileType = 1;
                }
                else if (Random.value < changeProb)
                {
                    //pick a new random type
                    //this excludes 0 as an option, since 0 means `wall'
                    tileType = Random.Range(1, tileset.Count);
                }
                else
                {
                    //use the type of the tile we just hit
                    tileType = hit;
                }
                SetGridSpace(tileType, x, y);
                break;//we made a hit, leave this loop
            }
            //hit failed check the next spot
            startX += (int)dirTest.x;
            startY += (int)dirTest.y;
            if (startX >= mapWidth && dirTest.x == 1)
            {
                //we walked off the right, loop back around
                startX = 0;
            }
            else if (startX <= 0 && dirTest.x == -1)
            {
                //we walked off left, loop around
                startX = mapWidth - 1;
            }
            else if (startY >= mapHeight && dirTest.y == 1)
            {
                startY = 0;
            }
            else if (startY <= 0 && dirTest.y == -1)
            {
                startY = mapHeight - 1;
            }
            //Debug.LogFormat("Miss! Trying again from ({0}, {1}) towards {2}.", startX, startY, dirFire);
        }

        //check if this was the last particle
        particlesFired++;
        return (particlesFired == numParticles);
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
        while (x > 0 &&
            x < mapWidth &&
            y > 0 &&
            y < mapHeight)
        {
            if (idGrid[x, y] != 0)
            {
                //we hit a non wall!
                int hitType = idGrid[x, y];
                //back up to where we came from (e.g. the tile we are about to modify)
                x -= (int)dirFire.x;
                y -= (int)dirFire.y;
                return hitType;
            }
            x += (int)dirFire.x;
            y += (int)dirFire.y;
        }
        return -1;
    }
}
