using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Perlin Noise Terrain generator
 * 
 * Samples random tiles from a Perlin Noise map.
 * The map can be "zoomed" or "panned"
 * by adjusting a few paramters.
 */
public class TGPerlin : TerrainGenerator
{

    [SerializeField] int seed = 1234; 
    [SerializeField] float magnification = 7f;
    //really these two parameters act as "seeds" as well
    //we don't really need "seed" itself
    [SerializeField] int xOffset = 0;
    [SerializeField] int yOffset = 0;

    protected override bool Step()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int tileType = GridIDUsingPerlin(x, y);
                idGrid[x, y] = tileType;
            }
        }

        return true;
    }

    private int GridIDUsingPerlin(int x, int y)
    {
        float rawPerlin = Mathf.PerlinNoise(seed + (x-xOffset)/magnification,
                            (y-yOffset)/magnification);

        rawPerlin = Mathf.Clamp01(rawPerlin);
        //just sets each tile type to an equal part of the 0 to 1 Perlin space
        float finalVal = Mathf.Clamp((rawPerlin * tileset.Count), 0, tileset.Count - 1);
        //we could alternatively change the weights here, e.g set anything 0 to 0.4 as "Plains"

        return Mathf.FloorToInt(finalVal);
    }

    //for regenerating as we mess with stuff in the editor
    //we dont use the 'rerun' flag anymore
    //since this is seeded
    private float oldSeed = -1;
    private float oldMagnification = -1;
    private int oldXOffset = -1;
    private int oldYOffset = -1;

    private void Update()
    {
        if (oldSeed == -1 || oldMagnification == -1 || oldXOffset == -1 || oldYOffset == -1)
        {
            oldSeed = seed;
            oldMagnification = magnification;
            oldXOffset = xOffset;
            oldYOffset = yOffset;
        }

        if (oldSeed != seed || oldMagnification != magnification || oldXOffset != xOffset || oldYOffset != yOffset)
        {
            Regenerate();
            oldSeed = seed;
            oldMagnification = magnification;
            oldXOffset = xOffset;
            oldYOffset = yOffset;
        }
    }

}
