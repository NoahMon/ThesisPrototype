using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public List<GameObject> gemPrefabs; // List of prefabs to spawn
    public int numberOfGems = 6; // Number of gems to spawn
    public Vector3 spawnAreaCenter; 
    public Vector3 spawnAreaSize;

    void Start()
    {
        SpawnGems();
    }

    void SpawnGems()
    {
        List<GameObject> gems = new List<GameObject>();

        for (int i = 0; i < numberOfGems; i++)
        {
            // Randomly select a gem prefab from the list
            GameObject selectedPrefab = gemPrefabs[Random.Range(0, gemPrefabs.Count)];

            // Random position within the spawn area
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                spawnAreaCenter.y,
                Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
            );

            // Instantiate gem at the random position
            GameObject gemInstance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
            gems.Add(gemInstance);
        }
    }
}
