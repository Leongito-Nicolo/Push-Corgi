using UnityEngine;

public enum Direction
{
    Orizontal,
    Vertical
}
public class BlockScripting : MonoBehaviour
{

    private GameManager _gameManager;
    //posizione attuale della griglia
    private Vector2Int _boardPosition;
    //dimensione celle
    private Vector2Int cellDimension;
    private MovementDirection _movementDirection;
    private bool isCharacter;
    
    public Vector2Int CurrentPoosition
    {
        get { return _boardPosition; }
        //usato quando il movimento è valido
        set { _boardPosition = value; }
    }

    public void SetUp(
        Vector2Int startPosition,
        Vector2Int dimension,
        MovementDirection direction,
        bool isTarget)
    {
        _boardPosition = startPosition;
        cellDimension = dimension;
        _movementDirection = direction;

    }

}
