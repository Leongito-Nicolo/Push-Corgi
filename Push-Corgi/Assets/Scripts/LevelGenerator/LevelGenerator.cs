using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //[Header("riferimenti prefab")]
    //public TypeBlockData blockMappingPrefab;
    //public GameObject sofaBlock;


    
    public List<GameObject> prefabBlocks;

    public GameObject exitPrefab;

    private float gridScale = 0.75f;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public void LevelGenerate(LevelData _levelData, int line, int col)

    {
        SceneCleaner();
        //ExitGenerator(_levelData.principalExit.ToVector2Int());
        BlockGenerator(_levelData, line, col);
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

    private void BlockGenerator(LevelData _levelData, int line, int col)
    {
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

                    Vector2Int startPosition = new Vector2Int(x, y);
                    
                    Vector3 worldPos = GridToWorldPosition(startPosition, line, col);

                    int prefabIndex = IDBlock - 1;

                    if (prefabIndex >= 0 && prefabIndex < prefabBlocks.Count)
                    {
                        GameObject prefabToSpawn = prefabBlocks[prefabIndex];
                        GameObject newBlockGo = Instantiate(prefabToSpawn, worldPos, prefabToSpawn.transform.rotation);

                        _spawnedObjects.Add(newBlockGo);
                         
                        //dimensione celle
                        Vector3 scale = new Vector3(
                            details.dimension.x * gridScale,
                            1f,
                            details.dimension.y * gridScale
                        );
                        newBlockGo.transform.localScale = scale;

                        //BlockSetUp(newBlockGo, details);

                        //gestione della direzione di movimento

                        //Draggable draggable = newBlockGo.GetComponent<Draggable>();

                        /*if (draggable != null)
                        {
                            draggable.SetUp(
                                startPosition,



                            );
                        }*/
    
                    }


                }
            }
        }
        
    }

    private Vector3 GridToWorldPosition(Vector2Int gridPos, int line, int col)
    {

        float y_original = gridPos.y;

        float x_inverted = col - 1 - gridPos.x;

        return new Vector3(y_original, 0.75f, x_inverted);
    }
    
    /*public void BlockSetUp(GameObject newBlockGo, BlockDettails dettails)
    {
        //gestione della direzione di movimento

        Draggable draggable = newBlockGo.GetComponent<Draggable>();

        if (draggable == null)
            return;

        draggable.Direction = dettails.direction;
    }*/

    
}
