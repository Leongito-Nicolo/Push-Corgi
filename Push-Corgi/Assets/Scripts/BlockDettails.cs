using System;
using UnityEngine;


/*public enum DirectionsMovement
{
    Orizontal,
    Vertical
}*/

[Serializable]
public class BlockDettails
{
    public int id;
    public string name;
    public Vector2Int dimension;
    
    public Direction direction;

    public bool character;
}
