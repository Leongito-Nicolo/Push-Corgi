using System;
using UnityEngine;


public enum DirectionsMovement
{
    Orizontal,
    Vertical
}

[Serializable]
public class BlockDettails
{
    public int id;
    public string name;
    public int dimension;
    public DirectionsMovement direction;

    public bool character;
}
