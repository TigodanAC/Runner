using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject groundTilePrefab;

    int initialTiles = 10;
    float tileLength = 30f;
    int tilesAhead = 10;
    int tilesBehind = 2;

    private LinkedList<GameObject> activeTiles = new LinkedList<GameObject>();
    private float z = 0f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        for (int i = 0; i < initialTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player == null) return;

        float playerZ = player.position.z;
        while (z - playerZ < tilesAhead * tileLength)
        {
            SpawnTile();
        }

        while (activeTiles.Count > 0)
        {
            GameObject first = activeTiles.First.Value;
            if (playerZ - first.transform.position.z > (tilesBehind + 0.5f) * tileLength)
            {
                Destroy(first);
                activeTiles.RemoveFirst();
            }
            else break;
        }
    }

    void SpawnTile()
    {
        Vector3 pos = new Vector3(0f, 0f, z);
        GameObject tile = Instantiate(groundTilePrefab, pos, Quaternion.identity);
        activeTiles.AddLast(tile);
        z += tileLength;
    }
}
