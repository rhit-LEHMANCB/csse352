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
      return true; 
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
        
        return -1;
    }
}
