using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class is used to map the possible neighbors to each tile.
 *  Each of these arrays is connected in the editor to other prefabs.
 */
public class Tile : MonoBehaviour
{
    public Tile[] upNeighbors;
    public Tile[] rightNeighbors;
    public Tile[] downNeighbors;
    public Tile[] leftNeighbors;
}
