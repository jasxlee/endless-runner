 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = -40.0f;
    private float titleLength = 80.0f;
    private float safeZone = 83.0f;
    private int amnTilesOnScreen = 7;
    private int lastPrefabIndex = 0;

    public List<GameObject> activesTiles;
    //Start is called before the first frame update
    void Start()
    {
        activesTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i = 0; i< amnTilesOnScreen; i++)
        {
            if (i < 2)
            {
                 SpawnTile (0);
            }
            else
            {
                SpawnTile ();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone> (spawnZ - amnTilesOnScreen * titleLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate (tilePrefabs [prefabIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += titleLength;
        activesTiles.Add(go);
    }
    private void DeleteTile()
    {
        Destroy(activesTiles[0]);
        activesTiles.RemoveAt(0);
    }
    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length  <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length );
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
