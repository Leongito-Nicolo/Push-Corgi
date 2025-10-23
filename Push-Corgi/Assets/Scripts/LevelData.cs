using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public Vector2Int principalExit;
    public int moveCounter;
    public int[] layoutData;
    public List<BlockDettails> data;
}
