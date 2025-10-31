using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public List<GameObject> prefabBlocks;

    public GameObject gridPrefab;

    public GameObject exitPrefab;

    private float gridScale = 1f;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public static LevelGenerator Instance { get; private set; }

    public Vector3 FixedExitPosition = new Vector3(6, 0, 3);

    public Vector3 FixedGridPosition = new Vector3(0, 0, 0);


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }



    public void LevelGenerate(LevelData _levelData, int line, int col)

    {
        SceneCleaner();
        SpawnExitPrefab();
        BlockGenerator(_levelData, line, col);
        SpawnGridRightPosition();
        //GridGenerator(line, col);
    }

    public void SceneCleaner()
    {
        Debug.Log($"Tentativo di pulizia. Oggetti tracciati: {_spawnedObjects.Count}");

        foreach (GameObject obj in _spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        _spawnedObjects.Clear();
        Debug.Log("Pulizia della scena precedente completata.");
    }

    public void BlockGenerator(LevelData _levelData, int line, int col)
    {
        float halfGridScale = gridScale / 2f;

        int[] layout = _levelData.layoutData;

        Dictionary<int, BlockDettails> blockMapDetails = new Dictionary<int, BlockDettails>();

        foreach (var details in _levelData.data)
        {
            blockMapDetails.Add(details.id, details);
        }

        //lettura del file JSON colonna per riga
        for (int y = 0; y < line; y++)
        {
            for (int x = 0; x < col; x++)
            {
                int index = x * col + y;
                int IDBlock = layout[index];


                if (IDBlock > 0 && blockMapDetails.ContainsKey(IDBlock))
                {
                    BlockDettails details = blockMapDetails[IDBlock];

                    Direction courrentDirection;

                    if (details.dimension.x > details.dimension.y)
                    {
                        courrentDirection = Direction.Horizontal;
                    }
                    else
                    {
                        courrentDirection = Direction.Vertical;
                    }

                    Vector2Int startPosition = new Vector2Int(x, y);

                    Vector3 worldPos = GridToWorldPosition(startPosition, line, col, courrentDirection, IDBlock);

                    int prefabIndex = IDBlock - 1;



                    if (prefabIndex >= 0 && prefabIndex < prefabBlocks.Count)
                    {
                        GameObject prefabToSpawn = prefabBlocks[prefabIndex];

                        GameObject newBlockGo = Instantiate(prefabToSpawn, worldPos, prefabToSpawn.transform.rotation);

                        _spawnedObjects.Add(newBlockGo);

                    }

                }
            }
        }
    }

    private void SpawnGridRightPosition()
    {
        Vector3 worldPos = FixedGridPosition;
        GameObject gridGo = Instantiate(gridPrefab, worldPos, gridPrefab.transform.rotation);
        _spawnedObjects.Add(gridGo);
    }

    private void SpawnExitPrefab()
    {
        if (exitPrefab == null)
    {
        Debug.LogWarning("Exit Prefab non assegnato in LevelGenerator. Assegnalo nell'Inspector.");
        return;
    }

        Vector3 worldPos = FixedExitPosition;

        GameObject exitGO = Instantiate(exitPrefab, worldPos, exitPrefab.transform.rotation);
        _spawnedObjects.Add(exitGO);
    }

    private Vector3 GridToWorldPosition(Vector2Int gridPos, int line, int col, Direction courrentDirection, int blockID)
    {

        float y_original = gridPos.y;
        float x_inverted = col - 1 - gridPos.x;

        float finalX = y_original * gridScale;
        float finalZ = x_inverted * gridScale;
        
        
        float finalY = 0f;


        if (blockID == 1) 
        {
            finalY += 0.5f; 
            finalZ -= 0.5f; 
        }
        if (blockID == 5)
        {
            finalZ -= 0.5f;
        }
        if (blockID == 3)
        {
            finalX -= 0.5f;
            finalZ += 0.15f;
        }
        
        
        return new Vector3(finalX, finalY, finalZ);
    }

    public void GridGenerator(int line, int col)
    {

        if (gridPrefab == null)
        {
            Debug.LogWarning("Il prefab della griglia non è assegnato. (campo 'Grid Prefab').");
            return;
        }

        float halfWidth = (col - 1) * gridScale / 2f;
        float halfDepth = (line - 1) * gridScale / 2f;

        Vector3 spawnPosition = new Vector3(
            halfDepth,
            0,
            halfWidth
        );

        GameObject gridGO = Instantiate(gridPrefab, spawnPosition, gridPrefab.transform.rotation);

        _spawnedObjects.Add(gridGO);
    }


}
