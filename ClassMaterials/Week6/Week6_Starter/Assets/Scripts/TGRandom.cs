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
       return true; 
    }

    protected override void Regenerate()
    {
        x = 0;
        y = 0;
        base.Regenerate();
    }
}
