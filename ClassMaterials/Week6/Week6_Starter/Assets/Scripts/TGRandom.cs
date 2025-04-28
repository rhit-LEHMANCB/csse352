using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGRandom : TerrainGenerator
{
    //current tile info
    int x;
    int y;

    protected override bool Step()
    {
        SetGridSpace(Random.Range(0, tileset.Count), x, y);

        x++;
        if (x >= mapWidth)
        {
            x = 0;
            y++;
            if (y >= mapHeight)
            {
                return true;
            }
        }
        return false;
    }

    protected override void Regenerate()
    {
        x = 0;
        y = 0;
        base.Regenerate();
    }
}
