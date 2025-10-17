using UnityEngine;


public class Vector2IntData
{
    public int x;
    public int y;

    public Vector2IntData(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2Int ToVectorInt()
    {
        return new Vector2Int(x, y);
    }
}
