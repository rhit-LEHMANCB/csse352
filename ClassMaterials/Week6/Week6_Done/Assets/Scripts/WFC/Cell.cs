using UnityEngine;

/*
 * This class is used as an intermediate datastructure.
 * It keeps track of the possible tiles at each location.
 * When only one option is left, the tile "collapses".
 */
public class Cell : MonoBehaviour
{
    public Tile errorTile;
    public bool collapsed;
    public Tile[] tileOptions;
    public int x, y;

    public void SetCell(bool collapseState, Tile[] tiles, int x, int y)
    {
        collapsed = collapseState;
        tileOptions = tiles;
        this.x = x;
        this.y = y;
    }

    public void SetTiles(Tile[] tiles)
    {
        tileOptions = tiles;
    }

    public int Entropy()
    {
        return tileOptions.Length;
    }

    public Tile CollapseTileRandomly()
    {
        tileOptions = new Tile[] { ChooseRandomTile() };
        collapsed = true;
        return tileOptions[0];
    }

    public Tile ChooseRandomTile()
    {
        //this is important for debugging.
        //in this example project it *will* occasionally happen
        //not all possible tiles are provided in our sprite sheet.
        if(tileOptions.Length == 0)
        {
            Debug.Log("Error on collapse, no tiles remaining. Inserting error tile.");
            return errorTile;
        }
        int index = Random.Range(0, tileOptions.Length);
        return tileOptions[index];
    }

    public override string ToString()
    {
        string tileNames = "";
        foreach (Tile t in tileOptions)
        {
            tileNames += " " + t.gameObject.name;
        }

        return string.Format("Cell ({0}, {1})", x, y);
    }
    
}
